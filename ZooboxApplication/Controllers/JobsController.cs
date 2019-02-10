using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Data;
using ZooboxApplication.Models;

namespace ZooboxApplication.Controllers
{   
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Controlador de páginas Tarefas. </summary>
    ///
    /// <remarks>   André Silva, 09/12/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Construtor do control Tarefas </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        ///
        /// <returns>   Construtor da class Tarefas  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public JobsController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Index com todos as Tarefas na plataforma </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        ///
        /// <returns>   Retorna uma view com todos as Tarefas  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Job.Include(j => j.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Detais, recebe o ID da tarefa em questão e procura a sua informação </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da Tarefa </param>
        /// <returns>   Retorna uma view com a informação detalhada de uma tarefa  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Create, retorna a view para criar uma tarefa </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        ///
        /// <returns>   Retorna uma view para criar uma tarefa  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Create()
        {
            ViewData["ApplicationUsers"] = new SelectList(_context.ApplicationUser, "Id", "Email");
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Create, endpoint para criar uma tarefa recebemdo todos os argumentos necessarios. </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        ///
        /// <param name="Id"> Id da tarefa </param>
        /// <param name="Abbreviation"> Abreviação da Tarefa </param>
        /// <param name="Description"> Descrição da Tarefa </param>
        /// <param name="BeginDay"> Data de inicio da Tarefa </param>
        /// <param name="EndDay"> Fim da Data da Tarefa</param>
        /// <param name="State"> Estado da Tarefa </param>
        /// <param name="UserId"> Id do utilizador </param>
        /// <returns>   Retorna um view com os Detalhes da nova tarefa  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Abbreviation,Description,BeginDay,EndDay,State,UserId")] Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                if(Request != null)
                {
                    var callbackUrl = Request.Scheme + "://" + Request.Host.Value + "/jobs/Edit/" + job.Id;
                    var email = _context.Users.Find(job.UserId).Email;
                    await _emailSender.SendEmailAsync(
                       email,
                       "Zoobox - Nova Tarefa",
                       $"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Por favor veja a tarefa criada.</a>.");
                    ViewData["ApplicationUsers"] = new SelectList(_context.ApplicationUser, "Id", "Email");
                }
               
                return RedirectToAction(nameof(Index));
            }

            return View(job);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Edit, endpoint para procurar a informação e devolve de uma Tarefa para que se possa editar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da Tarefa </param>
        /// <returns>   Retorna um view com o formulario preenchido para editar.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Edit(int id)
        {

            var job = await _context.Job.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Email", job.UserId);
            return View(job);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Edit, endpoint que recebe a informação relativa a uma Tarefa e edit essa Tarefa.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        ///
        /// <param name="Id"> Id da tarefa </param>
        /// <param name="Abbreviation"> Abreviação da Tarefa </param>
        /// <param name="Description"> Descrição da Tarefa </param>
        /// <param name="BeginDay"> Data de inicio da Tarefa </param>
        /// <param name="EndDay"> Fim da Data da Tarefa</param>
        /// <param name="State"> Estado da Tarefa </param>
        /// <param name="UserId"> Id do utilizador </param>
        ///
        /// <returns>   Retorna uma view com a Tarefa editado.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Abbreviation,Description,BeginDay,EndDay,State,UserId")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Email", job.UserId);
            return View(job);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Delete, recebe um id de uma Tarefa e devolve um view com a informação do mesmo e a opção para apagar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da Tarefa </param>
        /// <returns>   Retorna uma view com a Tarefa.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.ApplicationUser)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Delete, recebe um id de uma Tarefa e apaga essa Tarefa.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da Tarefa </param>
        /// <returns>   Retorna o index das Tarefa.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var job = await _context.Job.FindAsync(id);
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Verifica se uma Tarefa existe.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da Tarefa </param>
        /// <returns>   Retorna true ou false  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.Id == id);
        }
    }
}
