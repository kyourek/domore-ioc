using NUnit.Framework;
using System;

namespace Domore.IoC {
    [TestFixture]
    public class InjectTest {
        private class DataLayer {
        }

        private class AppLayer {
        }

        [Test]
        public void None_RemovesInjection() {
            Inject.Type<DataLayer>().Into<AppLayer>();
            Inject.None.Into<AppLayer>();
            Assert.Throws<InvalidOperationException>(() => {
                Injection<DataLayer>.For<AppLayer>();
            });
        }

        [Test]
        public void Type_InstantiatesType() {
            Inject.None.Into<AppLayer>();
            Inject.Type<DataLayer>().Into<AppLayer>();
            var inst = Injection<DataLayer>.For<AppLayer>();
            Assert.That(inst, Is.Not.Null);
        }

        [Test]
        public void Type_InstantiatesNewInstance() {
            Inject.None.Into<AppLayer>();
            Inject.Type<DataLayer>().Into<AppLayer>();
            var inst1 = Injection<DataLayer>.For<AppLayer>();
            var inst2 = Injection<DataLayer>.For<AppLayer>();
            Assert.That(inst2, Is.Not.SameAs(inst1));
        }

        [Test]
        public void This_ProvidesSameInstanceToType() {
            var data = new DataLayer();
            Inject.This(data).Into<AppLayer>();
            var inst = Injection<DataLayer>.For<AppLayer>();
            Assert.That(inst, Is.SameAs(data));
        }
    }
}
