namespace FateEngine {
    public class Aspect {
        public string Name {get; private set;}

        public Aspect(string name) {
            Name = name;
        }

        public override string ToString()
        {
            return $"Aspect[{Name}]";
        }
    }
}