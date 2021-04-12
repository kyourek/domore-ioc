using NUnit.Framework;

namespace Domore.IoC {
    [TestFixture]
    public class InjectionTest {
        private class InvisibleType {
            public class Controller {
            }
        }

        [Test]
        public void Into_UsesTypeName() {
            var obj1 = new InvisibleType.Controller();
            var subject = new Injection<InvisibleType.Controller>(() => obj1);
            subject.Into("InvisibleType");
            var obj2 = Injection<InvisibleType.Controller>.For(new InvisibleType());
            Assert.That(obj2, Is.SameAs(obj1));
        }

        private class Dependent {
            public ControllerClass Controller => Injection<ControllerClass>.For(this);
            public class ControllerClass {
            }
        }

        [Test]
        public void Into_UsesDerivedType() {
            var expected = new Dependent.ControllerClass();
            Inject.This(expected).Into(typeof(object));
            var actual = new Dependent().Controller;
            Assert.That(actual, Is.SameAs(expected));
        }
    }
}
