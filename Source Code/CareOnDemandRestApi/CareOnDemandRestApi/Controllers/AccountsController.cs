using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareOnDemandRestApi.Models;

namespace CareOnDemandRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly CareOnDemandContext _context;

        public AccountsController(CareOnDemandContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/Accounts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // GET: api/Accounts/email/{email}
        [HttpGet("email/{email}")]
        public async Task<ActionResult<Account>> GetAccount(string email)
        {
            var account = await _context.Accounts.Where(a => a.Email == email).ToListAsync();

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/Accounts/{id}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.AccountID)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accounts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetAccount", new { id = account.AccountID }, account);
            return CreatedAtAction(nameof(GetAccount), new { id = account.AccountID }, account);
        }

        // DELETE: api/Accounts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Account>> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return account;
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountID == id);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AccountLevelsController : ControllerBase
    {
        private readonly CareOnDemandContext _context;

        public AccountLevelsController(CareOnDemandContext context)
        {
            _context = context;
        }

        // GET: api/AccountLevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountLevel>>> GetAccountLevels()
        {
            return await _context.AccountLevels.ToListAsync();
        }

        // GET: api/AccountLevels/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountLevel>> GetAccountLevel(int id)
        {
            var accountLevel = await _context.AccountLevels.FindAsync(id);

            if (accountLevel == null)
            {
                return NotFound();
            }

            return accountLevel;
        }

        // PUT: api/AccountLevels/{id}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountLevel(int id, AccountLevel accountLevel)
        {
            if (id != accountLevel.AccountLevelID)
            {
                return BadRequest();
            }

            _context.Entry(accountLevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountLevelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AccountLevels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AccountLevel>> PostAccountLevel(AccountLevel accountLevel)
        {
            _context.AccountLevels.Add(accountLevel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccountLevel", new { id = accountLevel.AccountLevelID }, accountLevel);
        }

        // DELETE: api/AccountLevels/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountLevel>> DeleteAccountLevel(int id)
        {
            var accountLevel = await _context.AccountLevels.FindAsync(id);
            if (accountLevel == null)
            {
                return NotFound();
            }

            _context.AccountLevels.Remove(accountLevel);
            await _context.SaveChangesAsync();

            return accountLevel;
        }

        private bool AccountLevelExists(int id)
        {
            return _context.AccountLevels.Any(e => e.AccountLevelID == id);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly CareOnDemandContext _context;

        public AdminsController(CareOnDemandContext context)
        {
            _context = context;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        // GET: api/Admins/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Admins/{id}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            if (id != admin.AdminID)
            {
                return BadRequest();
            }

            _context.Entry(admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Admins
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmin", new { id = admin.AdminID }, admin);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Admin>> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return admin;
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.AdminID == id);
        }
    }

     [Route("api/[controller]")]
    [ApiController]
    public class CarePartnersController : ControllerBase
    {
        private readonly CareOnDemandContext _context;

        public CarePartnersController(CareOnDemandContext context)
        {
            _context = context;
        }

        // GET: api/CarePartners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarePartner>>> GetCarePartners()
        {
            return await _context.CarePartners.ToListAsync();
        }

        // GET: api/CarePartners/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CarePartner>> GetCarePartner(int id)
        {
            var carePartner = await _context.CarePartners.FindAsync(id);

            if (carePartner == null)
            {
                return NotFound();
            }

            return carePartner;
        }

        // PUT: api/CarePartners/{id}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarePartner(int id, CarePartner carePartner)
        {
            if (id != carePartner.CarePartnerID)
            {
                return BadRequest();
            }

            _context.Entry(carePartner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarePartnerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CarePartners
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CarePartner>> PostCarePartner(CarePartner carePartner)
        {
            _context.CarePartners.Add(carePartner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarePartner", new { id = carePartner.CarePartnerID }, carePartner);
        }

        // DELETE: api/CarePartners/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarePartner>> DeleteCarePartner(int id)
        {
            var carePartner = await _context.CarePartners.FindAsync(id);
            if (carePartner == null)
            {
                return NotFound();
            }

            _context.CarePartners.Remove(carePartner);
            await _context.SaveChangesAsync();

            return carePartner;
        }

        private bool CarePartnerExists(int id)
        {
            return _context.CarePartners.Any(e => e.CarePartnerID == id);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CareOnDemandContext _context;

        public CustomersController(CareOnDemandContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/{5}
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/{5}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerID }, customer);
        }

        // DELETE: api/Customers/{5}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}
