using System;

namespace Domore.IoC {
    public abstract class Injection {
        private static readonly InjectionRecipientCollection Collection = new InjectionRecipientCollection();

        internal static Injection None { get; } = new Remove();

        public Type ControlType { get; }

        public Injection(Type controlType) {
            ControlType = controlType;
        }

        public virtual void Into(object recipient) {
            Collection.Add(this, recipient);
        }

        public void Into<T>() {
            Into(typeof(T));
        }

        public abstract object Access();

        public TControl Access<TControl>() {
            var obj = Access();
            if (obj is TControl control) return control;
            throw new InvalidOperationException($"{nameof(Access)} returned an invalid object.");
        }

        public static InjectionCollection For(object recipient) {
            return Collection.Injections(recipient);
        }

        public static InjectionCollection For<T>() {
            return For(typeof(T));
        }

        public static TControl Access<TControl>(object recipient) {
            return For(recipient).Access<TControl>();
        }

        private class Remove : Injection {
            public Remove() : base(null) {
            }

            public override object Access() {
                return null;
            }

            public override void Into(object recipient) {
                Collection.Remove(recipient);
            }
        }
    }

    public class Injection<TControl> : Injection {
        public Func<TControl> AccessFunction { get; }

        public Injection(Func<TControl> accessFunction) : base(typeof(TControl)) {
            AccessFunction = accessFunction ?? throw new ArgumentNullException(nameof(accessFunction));
        }

        public override object Access() {
            return AccessFunction();
        }

        public static new TControl For(object recipient) {
            return Access<TControl>(recipient);
        }

        public static new TControl For<T>() {
            return For(typeof(T));
        }
    }
}
