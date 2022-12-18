using Domain;
using Infrastructure.EF.DbEntities;
using Infrastructure.EF.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTroc.Controllers;

[ApiController]
[Route("api/v1/Transaction")]

public class TransactionController : ControllerBase
{
    private readonly ITransaction _ITransaction;

    public TransactionController(ITransaction iTransaction)
    {
        _ITransaction = iTransaction;
    }

    [HttpGet]
    public IEnumerable<DbTransaction> GetAll()
    {
        return _ITransaction.GetAll();
    }
    
    //idUser1 = celui qui envoie la demande
    //iduser2 = celui qui recoit la demande
    
    //article1 = ce que le demandeur propose comme article
    //article2 = ce que le demandeur veut comme article
    [HttpPost]
    public ActionResult<Transaction> Create( int Id_user1, int Id_user2, string Article1, string Article2)
    {
        return Ok(_ITransaction.Create( DateTime.Now, Id_user1, Id_user2, Article1,Article2));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(DbTransaction dbTransaction)
    {
        return _ITransaction.Update(dbTransaction) ? NoContent() : NotFound();
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id)
    {
        return _ITransaction.Delete(id) ? NoContent() : NotFound();
    }
    
    [HttpGet]
    [Route("fetchByIdUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public IEnumerable<DbTransaction> FetchByIdUsers()
    {
        var id = User.Claims.First(claim => claim.Type == "id").Value;
        
        var idUser = Convert.ToInt32(id);
        return _ITransaction.fetchByIdUser(idUser);

    }
    
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DbTransaction> FetchById(int id)
    {
        try
        {
            return _ITransaction.fetchById(id);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }

}