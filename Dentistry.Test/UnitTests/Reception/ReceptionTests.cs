using Dentistry.Entities.Models;
using Dentistry.Repository;
using Dentistry.Services.Abstract;
using NUnit.Framework;

namespace Dentistry.Test;

[TestFixture]
public partial class ReceptionTests : UnitTest
{
    private  IReceptionService receptionService;
    private  IRepository<Entities.Models.Reception> receptionRepository;

    public async override Task OneTimeSetUp()
    {
        await base.OneTimeSetUp();
        receptionService = services.Get<IReceptionService>();
        receptionRepository = services.Get<IRepository<Entities.Models.Reception>>();
    }

    protected async override Task ClearDb()
    {
        var receptionRepository = services.Get<IRepository<Entities.Models.Reception>>();
        var receptions = receptionRepository.GetAll().ToList();
        foreach(var reception in receptions)
        {
            receptionRepository.Delete(reception);
        }
    }

}