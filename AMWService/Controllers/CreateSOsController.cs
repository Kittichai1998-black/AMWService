using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using AMWService.DbContext;
using AMWService.Models;
using System.IO;

namespace AMWService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateSOsController : ControllerBase
    {
        //private string formatDate = "dd/MM/yyyy";
        private readonly  DbConfig _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CreateSOsController(DbConfig context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }
        //GET: api/CreateSOs
        [HttpGet("GetColumns")]
        public async Task<ActionResult<IEnumerable<Columns>>> Getam_Status()
        {
            return await _context.am_Status.ToListAsync();
        }

        [HttpGet("GetServiceOrder")]
        public async Task<ActionResult<IEnumerable<CreateSO>>> Getams_ServiceOrder()
        {
            var val = (await _context.amv_ServiceOrders.ToListAsync());

            return Ok(val);
        }

        [HttpGet("GetSOs")]
        public async Task<ActionResult<IEnumerable<ViewServiceOrder>>> Getamv_ServiceOrders([FromQuery]DateTime Createtime)
        {
            if (Createtime.Date == default(DateTime))
            {
                return await _context.amv_ServiceOrders
                    .Select(x => new ViewServiceOrder()
                    {
                        ID = x.ID,
                        uuid = x.uuid,
                        SO = x.SO,
                        CustommerID = x.CustommerID,
                        Custommer = x.Custommer,
                        StatusID = x.StatusID,
                        Problem = x.Problem,
                        PriorityID = x.PriorityID,
                        priorityName = x.priorityName,
                        TypeID = x.TypeID,
                        RootcauseID = x.RootcauseID,
                        Image = x.Image,
                        UserID = x.UserID,
                        CreateTime = x.CreateTime,
                        //ModifyTime = x.ModifyTime,
                        //ImageFile = x.ImageFile,
                        ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.Image)
                    })
                    .ToListAsync();
            }
            else
            {
                var result = (await _context.amv_ServiceOrders
                    .Select(x => new ViewServiceOrder()
                    {
                        ID = x.ID,
                        uuid = x.uuid,
                        SO = x.SO,
                        CustommerID = x.CustommerID,
                        Custommer = x.Custommer,
                        StatusID = x.StatusID,
                        Problem = x.Problem,
                        PriorityID = x.PriorityID,
                        priorityName = x.priorityName,
                        TypeID = x.TypeID,
                        RootcauseID = x.RootcauseID,
                        Image = x.Image,
                        UserID = x.UserID,
                        CreateTime = x.CreateTime,
                        //ModifyTime = x.ModifyTime,
                        //ImageFile = x.ImageFile,
                        ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.Image)
                    })
                    .ToListAsync()).Where(x => x.CreateTime.Date == Createtime.Date).ToList();
                return Ok(result);
            }
        }


        [HttpGet("GetCustommer")]
        public async Task<ActionResult<IEnumerable<Project>>> Getam_Custommer()
        {
            return await _context.am_Custommer.ToListAsync();
        }
        [HttpGet("GetProblum")]
        public async Task<ActionResult<IEnumerable<Problem>>> Getam_Type()
        {
            return await _context.am_Type.ToListAsync();
        }
        [HttpGet("GetRootcause")]
        public async Task<ActionResult<IEnumerable<RootCause>>> Getam_RootCauseType()
        {
            return await _context.am_RootCauseType.ToListAsync();
        }

        [HttpGet("GetPriolity")]
        public async Task<ActionResult<IEnumerable<Priolity>>> Getam_Priority()
        {
            return await _context.am_Priority.ToListAsync();
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<IEnumerable<UserOwner>>> Getamv_User()
        {
            return await _context.amv_User.ToListAsync();
        }

        // PUT: api/CreateSOs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("ServiceID={id}")]
        public async Task<IActionResult> PutStatus(int id,[FromBody]UpdateStatus updateSO)
        {
            if (id != updateSO.ID)
            {
                return BadRequest();
            }

            _context.Entry(updateSO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CreateSOExists(id))
                //{
                    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return NoContent();
        }
        //    private bool CreateSOExists(int id)
        //{
        //    throw new NotImplementedException();
        //}

        // POST: api/CreateSOs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateSO>> PostCreateSO([FromForm] CreateSO createSO)
        {
            createSO.Image = await Uploadimage(createSO.ImageFile);
            _context.ams_ServiceOrder.Add(createSO);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Success" });
        }

        [NonAction]
        private async Task<string> Uploadimage(IFormFile files)
        {
            string Upload = null;
            if (files != null)
            {
                var path = Directory.GetCurrentDirectory() +"/Images/";
                Upload = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);
                string save = path + Upload;
                using (var stream = new FileStream(save, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }
            }
            return Upload;
        }
    }

      
    }

