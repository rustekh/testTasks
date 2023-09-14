using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AnimalsController : ControllerBase
{
    private IAnimalsRepository _animalsRepository;
    public AnimalsController(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }

    [HttpGet]
    public IEnumerable<Animal> Get()
    {
        return _animalsRepository.List();
    }

    [HttpPost]
    public void Post(Animal newAnimal)
    {
        _animalsRepository.Add(newAnimal);
    }

    [HttpDelete]
    [Route("{animalName}")]
    public void Delete(string animalName)
    {
        _animalsRepository.Delete(animalName);
    }
}