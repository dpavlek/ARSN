using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ARSN.Models;

namespace ARSN.Controllers
{
    public class RoundsController : Controller
    {
        private readonly DBContext _context;

        public RoundsController(DBContext context)
        {
            _context = context;
        }

        // GET: Rounds
        public async Task<IActionResult> Index(Guid? id)
        {

            if (id != null)
            {
                var thisRound = await _context.Round.Include(d => d.GameCollection).ThenInclude(x => x.HomeTeam)
                             .Include(d => d.GameCollection).ThenInclude(x => x.AwayTeam).Include(d=>d.Competition).SingleOrDefaultAsync(d => d.RoundID == id);
                if (!thisRound.Finished)
                {
                    bool Flag = false;
                    string SportType = null;
                    List<string> ListOfWinners = new List<string>();
                    int i = 0;
                    foreach (var temp in thisRound.GameCollection)
                    {
                        var game = await _context.Game.SingleOrDefaultAsync(d => d.GameID == temp.GameID);
                        SportType = game.Type;
                        if (game.Winner == null)
                        {
                            Flag = true;
                            break;
                        }
                        if (game.Winner == "Domaći") ListOfWinners.Add(game.HomeTeam.Name);
                        else ListOfWinners.Add(game.AwayTeam.Name);
                    }
                    if (Flag == true)
                    {
                        ModelState.AddModelError("Error", "Nisu unešeni svi rezultati!");
                        System.IO.File.AppendAllText(@"D:\error.txt", "Error nisu unesene sve igre!\n");
                    }
                    else
                    {
                        thisRound.Finished = true;
                        var NameOfOldRound = thisRound.Name;
                        Guid CurrentCompetition = thisRound.Competition.CompetitionID;
                        System.IO.File.AppendAllText(@"D:\cc.txt", CurrentCompetition.ToString());

                        string[] OldRoundLines = NameOfOldRound.Split(
                                new[] { "."},
                                StringSplitOptions.None
                         );
                        int.TryParse(OldRoundLines[0], out int NumOfNewRound);
                        NumOfNewRound++;
                        string NameOfNewRound = NumOfNewRound.ToString() + ".kolo";
                        if (ListOfWinners.LongCount() == 1)
                        {
                            System.IO.File.AppendAllText(@"D:\winner.txt", ListOfWinners.ToArray().GetValue(0).ToString()+"\n");
                            var LastCollection = await _context.Round.Include(d => d.GameCollection).ThenInclude(x => x.HomeTeam)
                             .Include(d => d.GameCollection).ThenInclude(x => x.AwayTeam).ToListAsync();
                            return View(LastCollection);
                        }
                        string[] lines = ListOfWinners.ToArray();

                       // for (i = 0; i < lines.Length; i++)
                            //System.IO.File.AppendAllText(@"D:\winner.txt", lines[i]);

                        Team HomeTeam = null, AwayTeam = null;
                        List<Game> ListGames = new List<Game>();
                        Round NextRound = new Round
                        {
                            Name = NameOfNewRound,
                            Finished = false
                        };
                        if (lines.Length % 2 == 1)
                        {
                            HomeTeam = await _context.Team.SingleAsync(d => d.Name == lines[lines.Length - 1]);
                            Game NewGame = new Game
                            {
                                HomeTeam = HomeTeam,
                                Type = SportType,
                                Winner = "Domaći"
                            };
                            _context.Game.Add(NewGame);
                            await _context.SaveChangesAsync();
                            ListGames.Add(NewGame);
                        }

                        for (i = 0; i < lines.Length - 1; i += 2)
                        {
                            HomeTeam = await _context.Team.SingleAsync(d => d.Name == lines[i]);
                            AwayTeam = await _context.Team.SingleAsync(d => d.Name == lines[i + 1]);
                            Game NewGame = new Game
                            {
                                HomeTeam = HomeTeam,
                                AwayTeam = AwayTeam,
                                Type = SportType
                            };

                            _context.Game.Add(NewGame);
                            await _context.SaveChangesAsync();
                            ListGames.Add(NewGame);
                            HomeTeam = null;
                            AwayTeam = null;
                        }
                       
                        NextRound.GameCollection = ListGames;
                        NextRound.Finished =false;
                        _context.Round.Add(NextRound);
                        await _context.SaveChangesAsync();
                        List<Round> ListRound = new List<Round>
                         {
                              NextRound
                         };
                        var competition = await _context.Competition.Include(d => d.RoundCollection).SingleOrDefaultAsync(d=>d.CompetitionID == CurrentCompetition);

                        competition.RoundCollection.Add(NextRound);
                         System.IO.File.WriteAllText(@"D:\competition.txt", competition.Name);
                        _context.Competition.Update(competition);
                        await _context.SaveChangesAsync(); 
                    }

                }
                else
                {
                    System.IO.File.AppendAllText(@"D:\locked.txt", "Kolo zavrseno\n");
                }
            }
          
            var gameCollection = await _context.Round.Include(d => d.GameCollection).ThenInclude(x => x.HomeTeam)
                             .Include(d => d.GameCollection).ThenInclude(x => x.AwayTeam).ToListAsync();
            return View(gameCollection);
        }

        // GET: Rounds/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var round = await _context.Round
                .SingleOrDefaultAsync(m => m.RoundID == id);
            if (round == null)
            {
                return NotFound();
            }

            return View(round);
        }

        // GET: Rounds/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Round
                .SingleOrDefaultAsync(m => m.RoundID == id);
            if (round == null)
            {
                return NotFound();
            }

            return View(round);
        }

        // POST: Rounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var round = await _context.Round.SingleOrDefaultAsync(m => m.RoundID == id);
            _context.Round.Remove(round);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoundExists(Guid id)
        {
            return _context.Round.Any(e => e.RoundID == id);
        }
    }
}
