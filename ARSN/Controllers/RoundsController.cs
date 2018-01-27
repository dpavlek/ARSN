using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ARSN.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ARSN.Controllers
{
    public class RoundsController : Controller
    {
        private readonly DBContext _context;
        const string CompetitionKey = "_CompetitionID";
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
                if (thisRound != null)
                {
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
                            ModelState.AddModelError("Error", "Nisu uneseni svi rezultati!");
                        }
                        else
                        {
                            thisRound.Finished = true;
                            var NameOfOldRound = thisRound.Name;
                            Guid CurrentCompetition = thisRound.Competition.CompetitionID;

                            string[] OldRoundLines = NameOfOldRound.Split(
                                    new[] { "." },
                                    StringSplitOptions.None
                             );
                            int.TryParse(OldRoundLines[0], out int NumOfNewRound);
                            NumOfNewRound++;
                            string NameOfNewRound = NumOfNewRound.ToString() + ".kolo";
                            if (ListOfWinners.LongCount() == 1)
                            {
                                var CompetID = new Guid(HttpContext.Session.GetString(CompetitionKey));
                                var LastCollection = await _context.Round.Include(d => d.GameCollection).ThenInclude(x => x.HomeTeam)
                                 .Include(d => d.GameCollection).ThenInclude(x => x.AwayTeam)
                                 .Where(d=>d.Competition.CompetitionID==CompetID)
                                 .ToListAsync();
                                ModelState.AddModelError("Error", "Pobjednik natjecanja je: " + ListOfWinners.ToArray().GetValue(0).ToString());
                                return View(LastCollection);
                            }
                            string[] lines = ListOfWinners.ToArray();
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
                            NextRound.Finished = false;
                            _context.Round.Add(NextRound);
                            await _context.SaveChangesAsync();
                            List<Round> ListRound = new List<Round>
                         {
                              NextRound
                         };
                            var competition = await _context.Competition.Include(d => d.RoundCollection).SingleOrDefaultAsync(d => d.CompetitionID == CurrentCompetition);

                            competition.RoundCollection.Add(NextRound);
                            foreach(var round in competition.RoundCollection)
                            _context.Competition.Update(competition);
                            await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Kolo je zavrseno !");
                    }
                }
                else
                {
                    var competition = await _context.Competition.SingleOrDefaultAsync(d => d.CompetitionID == id);
                    if (competition != null)
                    {
                        var Rounds = await _context.Round
                          .Include(d => d.GameCollection).ThenInclude(x => x.HomeTeam)
                          .Include(d => d.GameCollection).ThenInclude(x => x.AwayTeam)
                          .Where(d => d.Competition.CompetitionID == competition.CompetitionID)
                          .ToListAsync();
                        HttpContext.Session.SetString(CompetitionKey, id.ToString());
                         PopulateGraph(id);
                        return View(Rounds);
                    }
                    else return NotFound();
                }
            }
     
                var CompID = new Guid(HttpContext.Session.GetString(CompetitionKey));
                var gameCollection = await _context.Round
                   .Include(d => d.GameCollection).ThenInclude(x => x.HomeTeam)
                  .Include(d => d.GameCollection).ThenInclude(x => x.AwayTeam)
                  .Where(d => d.Competition.CompetitionID == CompID)
                  .ToListAsync();
            PopulateGraph(CompID);
            return View(gameCollection);
        }

        private async void PopulateGraph(Guid? id)
        {
            ViewBag.Rows = null;
            var RoundCollection = await _context.Round
                    .Include(d => d.GameCollection).ThenInclude(x => x.HomeTeam)
                   .Include(d => d.GameCollection).ThenInclude(x => x.AwayTeam)
                   .Where(d => d.Competition.CompetitionID == id)
                   .ToListAsync();
            int i = 0;
            string Current = "";
            string Previous = " ";
            List<string> Elemental = new List<string>();
            foreach (var round in RoundCollection)
            {
                foreach(var game in round.GameCollection)
                {
                    System.IO.File.AppendAllText(@"D:/game.txt", game.HomeTeam.Name);
                    Elemental.Add(game.HomeTeam.Name+Current);
                    if (game.Winner == "Domaći") Elemental.Add(game.HomeTeam.Name+Previous);
                    else if (game.Winner == "Gosti") Elemental.Add(game.AwayTeam.Name + Previous);
                    else Elemental.Add("");
                    if (game.AwayTeam != null)
                    {
                        Elemental.Add(game.AwayTeam.Name + Current);
                        if (game.Winner == "Domaći") Elemental.Add(game.HomeTeam.Name + Previous);
                        else if (game.Winner == "Gosti") Elemental.Add(game.AwayTeam.Name + Previous);
                        else Elemental.Add("");
                    }          
                }
                Current = Current + " ";
                Previous = Previous + " ";
            }

            ViewBag.Rows = JsonConvert.SerializeObject(Elemental);
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

            return RedirectToAction(nameof(Index));
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
