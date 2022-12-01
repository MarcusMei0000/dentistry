using Dentistry.Services.Models;

namespace Dentistry.Services.Abstract;

public interface IReceptionService
{
    ReceptionModel CreateReception(CreateReceptionModel createReceptionModel);
    ReceptionModel GetReception(Guid id);

    ReceptionModel UpdateReception(Guid id, UpdateReceptionModel reception);

    void DeleteReception(Guid id);

    PageModel<ReceptionPreviewModel> GetReceptions(int limit = 20, int offset = 0);
}