using MasterDetail_Api.DTO;
using MasterDetail_Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;

namespace MasterDetail_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingEntriesController : ControllerBase
{
    private readonly BookingDbContext _context;
    private readonly IWebHostEnvironment _env;

    public BookingEntriesController(BookingDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        this._env = env;
    }

    [HttpGet]
    [Route("GetSpots")]
    public async Task<ActionResult<IEnumerable<Spot>>> GetSpots()
    {
        return await _context.Spots.ToListAsync();
    }

    [HttpGet]
    [Route("GetClients")]
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        return await _context.Clients.Include(x => x.bookingEntries).ThenInclude(b => b.Spot).ToListAsync();
    }

    [HttpGet]
    [Route("GetClientById/{clientId}")]
    public async Task<ActionResult<Client>> GetClientById([FromRoute] int clientId)
    {
        return await _context.Clients
            .Where(x => x.ClientId == clientId)
            .Include(x => x.bookingEntries).ThenInclude(b => b.Spot)
            .FirstAsync();
    }

    // POST: api/BookingEntries
    [HttpPost]
    public async Task<IActionResult> PostBookingEntry([FromForm] ClientDTO clientDTO)
    {
        var clientData = JsonSerializer.Deserialize<ClientVM>(clientDTO.ClientInfo);

        Client client = new Client
        {
            ClientName = clientData.ClientName,
            BirthDate = clientData.BirthDate,
            PhoneNo = clientData.PhoneNo,
            MaritalStatus = clientData.MaritalStatus
        };

        if (clientDTO.PictureFile != null)
        {
            var webroot = _env.WebRootPath;
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(clientDTO.PictureFile.FileName);
            var filePath = Path.Combine(webroot, "Images", fileName);

            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await clientDTO.PictureFile.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            fileStream.Close();
            client.Picture = fileName;
        }

        foreach (var item in clientData.SpotList)
        {
            var bookingEntry = new BookingEntry
            {
                Client = client,
                ClientId = client.ClientId,
                SpotId = item.SpotId
            };
            _context.Add(bookingEntry);
        }

        await _context.SaveChangesAsync();

        return Ok();
    }

    // Update: api/BookingEntries
    [HttpPut]
    public async Task<IActionResult> UpdateBookingEntry([FromForm] ClientDTO clientDTO)
    {
        var clientData = JsonSerializer.Deserialize<ClientVM>(clientDTO.ClientInfo);

        Client client = new Client
        {
            ClientId = clientData.ClientId,
            ClientName = clientData.ClientName,
            BirthDate = clientData.BirthDate,
            PhoneNo = clientData.PhoneNo,
            MaritalStatus = clientData.MaritalStatus
        };

        if (clientDTO.PictureFile != null)
        {
            var webroot = _env.WebRootPath;
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(clientDTO.PictureFile.FileName);
            var filePath = Path.Combine(webroot, "Images", fileName);

            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await clientDTO.PictureFile.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            fileStream.Close();
            client.Picture = fileName;
        }

        _context.Clients.Update(client);

        var existingSpots = await _context.BookingEntries
            .Where(x => x.ClientId == client.ClientId).ToListAsync();

        if (existingSpots.Any())
        {
            _context.BookingEntries.RemoveRange(existingSpots);
        }

        foreach (var item in clientData.SpotList)
        {
            var bookingEntry = new BookingEntry
            {
                Client = client,
                ClientId = client.ClientId,
                SpotId = item.SpotId
            };
            _context.Add(bookingEntry);
        }

        await _context.SaveChangesAsync();

        return Ok();
    }



    //Delete Booking
    [Route("Delete/{id}")]
    [HttpDelete]
    public async Task<ActionResult<BookingEntry>> DeleteBookingEntry(int id)
    {

        Client client = _context.Clients.Find(id);

        var existingSpots = _context.BookingEntries.Where(x => x.ClientId == client.ClientId).ToList();
        foreach (var item in existingSpots)
        {
            _context.BookingEntries.Remove(item);
        }

        _context.Entry(client).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        return Ok(client);
    }


}
