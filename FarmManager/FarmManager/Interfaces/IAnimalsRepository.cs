public interface IAnimalsRepository
{
    List<Animal> List();
    void Add(Animal newAnimal);
    void Delete(string animalName);
}