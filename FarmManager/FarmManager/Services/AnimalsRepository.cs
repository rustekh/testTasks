public class AnimalsRepository : IAnimalsRepository
{
    private List<Animal> _animals = new List<Animal>();

    public void Add(Animal newAnimal)
    {
        Validate(newAnimal);

        newAnimal.Name = newAnimal.Name!.Trim();

        _animals.Add(newAnimal);
    }

    private void Validate(Animal newAnimal)
    {
        if (string.IsNullOrWhiteSpace(newAnimal.Name))
        {
            throw new ValidationException($"The animal name is empty.");
        }

        if (_animals.Any(a => a.Name == newAnimal.Name.Trim()))
        {
            throw new ValidationException($"The animal name is not unique: {newAnimal.Name}");
        }
    }

    public List<Animal> List() => _animals;

    public void Delete(string animalName)
    {
        _animals = _animals.Where(a => a.Name != animalName.Trim()).ToList();
    }
}