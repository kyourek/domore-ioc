using System.Collections.Generic;
using System.Linq;

namespace Domore.IoC {
    internal class InjectionRecipientCollection {
        private readonly List<InjectionRecipient> List = new List<InjectionRecipient>();

        public void Add(Injection injection, object recipient) {
            List.Add(InjectionRecipient.For(injection, recipient));
        }

        public InjectionCollection Injections(object recipient) {
            return new InjectionCollection(List
                .Where(item => item.Targets(recipient))
                .Select(item => item.Injection));
        }

        public void Remove(object recipient) {
            List.RemoveAll(item => item.Targets(recipient));
        }
    }
}
