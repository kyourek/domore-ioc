using System;

namespace Domore.IoC {
    internal class InjectionRecipient {
        public object Recipient { get; }
        public Injection Injection { get; }

        public InjectionRecipient(Injection injection, object recipient) {
            Injection = injection;
            Recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
        }

        public virtual bool Targets(object recipient) {
            return
                ReferenceEquals(Recipient, recipient) ||
                Recipient.Equals(recipient);
        }

        public static InjectionRecipient For(Injection injection, object recipient) {
            if (recipient is Type t) return new ForType(injection, t);
            if (recipient is string s) return new ForTypeName(injection, s);
            return new InjectionRecipient(injection, recipient);
        }

        private class ForType : InjectionRecipient {
            public Type Type { get; }

            public ForType(Injection injection, Type type) : base(injection, type) {
                Type = type ?? throw new ArgumentNullException(nameof(type));
            }

            public override bool Targets(object recipient) {
                return
                    Type.IsAssignableFrom(recipient?.GetType()) ||
                    base.Targets(recipient);
            }
        }

        private class ForTypeName : InjectionRecipient {
            public string TypeName { get; }

            public ForTypeName(Injection injection, string typeName) : base(injection, typeName) {
                TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
            }

            public override bool Targets(object recipient) {
                return
                    TypeName.Equals(recipient?.GetType()?.Name) ||
                    TypeName.Equals((recipient as Type)?.Name) ||
                    base.Targets(recipient);
            }
        }
    }
}
