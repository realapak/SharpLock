using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SharpLock.MongoDB
{
    public class MongoDataStore<TLockableObject> : IDataStore<TLockableObject, ObjectId>
        where TLockableObject : class, ISharpLockable<ObjectId>
    {
        private readonly MongoDataStore<TLockableObject, TLockableObject> _baseDataStore;

        public MongoDataStore(IMongoCollection<TLockableObject> col, ISharpLockLogger sharpLockLogger, TimeSpan lockTime)
        {
            _baseDataStore = new MongoDataStore<TLockableObject, TLockableObject>(col, sharpLockLogger, lockTime);
        }

        public ISharpLockLogger GetLogger() => _baseDataStore.GetLogger();
        public TimeSpan GetLockTime() => _baseDataStore.GetLockTime();

        public Task<TLockableObject> AcquireLockAsync(ObjectId baseObjId, TLockableObject obj, int staleLockMultiplier,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _baseDataStore.AcquireLockAsync(baseObjId, obj, x => x, staleLockMultiplier, cancellationToken);
        }

        public Task<bool> RefreshLockAsync(ObjectId baseObjId, Guid lockedObjectLockId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _baseDataStore.RefreshLockAsync(baseObjId, baseObjId, lockedObjectLockId, x => x, cancellationToken);
        }

        public Task<bool> ReleaseLockAsync(ObjectId baseObjId, Guid lockedObjectLockId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _baseDataStore.ReleaseLockAsync(baseObjId, baseObjId, lockedObjectLockId, x => x, cancellationToken);
        }

        public Task<TLockableObject> GetLockedObjectAsync(ObjectId baseObjId, Guid lockedObjectLockId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _baseDataStore.GetLockedObjectAsync(baseObjId, baseObjId, lockedObjectLockId, x => x,
                cancellationToken);
        }
    }
}