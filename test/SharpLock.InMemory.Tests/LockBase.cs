using System;
using System.Collections.Generic;

namespace SharpLock.InMemory.Tests
{
    public class LockBase : ISharpLockable<string>
    {
        public LockBase()
        {
            Id = "FooBar";
            SomeVal = "abcd1234";
            SingularInnerLock = new InnerLock();
            ListOfLockables = new List<InnerLock> { new InnerLock(), new InnerLock() };
            ArrayOfLockables = new[] { new InnerLock(), new InnerLock() };
            EnumerableLockables = new List<InnerLock> { new InnerLock(), new InnerLock() };
        }
        public string Id { get; set; }
        public DateTime? UpdateLock { get; set; }
        public Guid? LockId { get; set; }
        public string SomeVal { get; set; }
        public InnerLock SingularInnerLock { get; set; }
        public IList<InnerLock> ListOfLockables { get; set; }
        public InnerLock[] ArrayOfLockables { get; set; }
        public IEnumerable<InnerLock> EnumerableLockables { get; set;}
    }
}