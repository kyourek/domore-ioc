namespace Domore.IoC {
    public static class Inject {
        internal static Injection None => Injection.None;

        public static Injection This<TControl>(TControl control) {
            return new Injection<TControl>(() => control);
        }

        public static Injection Type<TControl>() where TControl : new() {
            return new Injection<TControl>(() => new TControl());
        }
    }
}
