namespace Common {
    public class Logger {
        public static void INFO(string s) {
            System.Console.WriteLine(s);
            Godot.GD.Print(s);
        }

        public static void ERROR(string s) {
            System.Console.Error.WriteLine(s);
            Godot.GD.PrintErr(s);
            Godot.GD.PushError(s);
        }
    }
}