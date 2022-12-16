using System;
using System.Collections.Generic;
using ECS;

namespace Asteroids.Asteroid
{
    public class AsteroidsSpawnSystem : ECS.System
    {
        public override IEnumerable<Type> ComponentsMask { get; }
        public override void Process(Entity entity)
        {
            
        }

        public override void PostProcess()
        {
        }

        public override void Destroy()
        {
        }
        
        
        /*private const float OVERLAP_RADIUS = 1.5f;
        private Collider2D[] _buffer;
        
        public AsteroidsManagerPresenter(AsteroidsManagerModel model, AsteroidsManagerView view) : base(model, view)
        {
            _buffer = new Collider2D[100];
        }

        public void SpawnAll()
        {
            Vector2 asteroidSpawnPosition;
            
            for (var i = 0; i < model.AsteroidsSpawnAmount; i++)
            {
                do
                {
                    asteroidSpawnPosition = model.FieldCalculationHelper.GetRandomPointFromInside();
                } while (Physics2D.OverlapCircleNonAlloc(asteroidSpawnPosition, OVERLAP_RADIUS, _buffer) != 0);

                var asteroidRotation = MathHelper.GetRandom2DRotation();

                var asteroidVelocity = asteroidRotation * Vector3.up 
                                                        * Random.Range(model.AsteroidVelocityRange.x, model.AsteroidVelocityRange.y);

                var movableData = new MovableData()
                {
                    accelerateForward = false,
                    acceleration = Vector2.zero,
                    destroyOutsideTheField = false,
                    friction = 0f,
                    position = asteroidSpawnPosition,
                    rotation = asteroidRotation,
                    velocity = asteroidVelocity
                };

                SpawnAsteroid(movableData, model.AsteroidPrefab);
            }
        }

        private void HandleCollision(object sender, GameObject inflictorGameObject)
        {
            var asteroidGO = ((CollisionTrackerFacade)sender).gameObject;
            var asteroidData = model.AsteroidsData.FirstOrDefault(asteroid => asteroid.GO == asteroidGO);

            if (MaskHelper.CheckIfLayerInMask(model.AsteroidIgnoreMask, inflictorGameObject.layer))
                return;
            
            if (asteroidData.GO == null)
            {
                Debug.LogError("No such record in list of asteroids!");
                return;
            }

            asteroidData.CollisionTracker.Collision -= HandleCollision;

            //If asteroid takes damage with projectile
            if (!asteroidData.IsFraction && MaskHelper.CheckIfLayerInMask(model.AsteroidDamageMask, inflictorGameObject.layer))
            {
                var rootPosition = asteroidData.GO.transform.position;
                var rootRotation = MathHelper.GetRandom2DRotation();

                var circleFraction = 369f / model.AsteroidsSpawnAmount;
                
                for (int i = 0; i < model.AsteroidsSpawnAmount; i++)
                {
                    var positionOffset = Quaternion.Euler(0f, 0f, circleFraction * i)  * (Vector3.up * 0.3f);
                    positionOffset = rootPosition + rootRotation * positionOffset;

                    var movableData = new MovableData()
                    {
                        accelerateForward = false,
                        acceleration = Vector2.zero,
                        destroyOutsideTheField = false,
                        friction = 0f,
                        mass = 1f,
                        position = positionOffset,
                        rotation = MathHelper.GetRandom2DRotation(),
                        velocity = Vector2.up
                    };
                        
                    SpawnAsteroid(movableData, model.AsteroidFragmentPrefab);
                }
                return;
            }
            asteroidData.Movable.DestroyEntity();
            //swapped to row above
            //model.MovableSystem.DeleteMovable(this, asteroidData.Movable);
            model.AsteroidsData.Remove(asteroidData);

            //destroy asteroid
        }

        private void SpawnAsteroid(MovableData movableData, GameObject prefab)
        {
            var asteroidGO = GameObject.Instantiate(prefab, movableData.position, 
                movableData.rotation);
            
            var movableFacade = model.MovableFactory.Create(movableData, asteroidGO, false);
            model.MovableSystem.AddMovable(this, movableFacade);
            var collisionTrackerFacade = model.CollisionTrackerFactory.Create(asteroidGO, false);
            
            AsteroidData asteroidData = new AsteroidData()
            {
                GO = asteroidGO,
                Movable = movableFacade,
                CollisionTracker = collisionTrackerFacade,
                IsFraction = false
            };
            collisionTrackerFacade.Collision += HandleCollision;
            model.AsteroidsData.Add(asteroidData);
        }

        public override void Destroy()
        {
            throw new System.NotImplementedException();
        }*/
    }
}
