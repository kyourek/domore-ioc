using NUnit.Framework;

namespace Domore.IoC {
    [TestFixture]
    public class InjectionTest {
        private class InvisibleType {
        }

        [Test]
        public void Into_UsesTypeName() {
            var obj1 = new object();
            var subject = new Injection<object>(() => obj1);
            subject.Into("InvisibleType");
            var obj2 = Injection<object>.For(new InvisibleType());
            Assert.That(obj2, Is.SameAs(obj1));
        }
    }
}
