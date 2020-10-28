using System;
using System.Collections.Generic;

namespace Domore.IoC {
    public class InjectionCollection {
        private readonly List<Injection> List;

        public InjectionCollection(IEnumerable<Injection> collection) {
            List = new List<Injection>(collection);
        }

        public TControl Access<TControl>() {
            foreach (var item in All<TControl>()) {
                return item;
            }
            throw new InvalidOperationException($"{nameof(Access)} not found");
        }

        public IEnumerable<TControl> All<TControl>() {
            foreach (var item in List) {
                if (typeof(TControl).IsAssignableFrom(item.ControlType)) {
                    yield return item.Access<TControl>();
                }
            }
        }
    }
}
