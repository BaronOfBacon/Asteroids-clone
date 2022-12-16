using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Collisions;
using Asteroids.Helpers;
using Asteroids.Movable;
using ECS;
using ECS.Messages;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Asteroid
{
    public class AsteroidsSpawnSystem : ECS.System
    {
        private const float OVERLAP_RADIUS = 1.5f;
        
        public override IEnumerable<Type> ComponentsMask => emptyList;

        private List<Type> emptyList = new List<Type>();

        private bool _isEnabled;
        private bool _isFirstStart = true;
        private List<AsteroidComponent> _asteroids = new List<AsteroidComponent>();
        private Collider2D[] _buffer;
        private float _newAsteroidSpawnTimeLeft;
        
        
        private GameObject _asteroidFractionPrefab;
        private GameObject _asteroidPrefab;
        private int _asteroidsInitSpawnAmount;
        private int _asteroidFragmentsSpawnAmount;
        private Vector2 _asteroidVelocityRange;
        private Vector2 _asteroidFragmentVelocityRange;
        private Vector2 _asteroidFragmentRandomAngleRange;
        private FieldCalculationHelper _fieldCalculationHelper;
        private float _newAsteroidSpawnCoolDown;

        public AsteroidsSpawnSystem(GameObject asteroidPrefab, GameObject asteroidFractionPrefab,
            int asteroidsInitSpawnAmount, int asteroidFragmentsSpawnAmount, Vector2 asteroidVelocityRange,
            Vector2 asteroidFragmentVelocityRange, Vector2 asteroidFragmentRandomAngleRange, 
            FieldCalculationHelper fieldCalculationHelper, float newAsteroidSpawnCoolDown)
        {
            _asteroidPrefab = asteroidPrefab;
            _asteroidFractionPrefab = asteroidFractionPrefab;
            _asteroidsInitSpawnAmount = asteroidsInitSpawnAmount;
            _asteroidFragmentsSpawnAmount = asteroidFragmentsSpawnAmount;
            _asteroidVelocityRange = asteroidVelocityRange;
            _asteroidFragmentVelocityRange = asteroidFragmentVelocityRange;
            _asteroidFragmentRandomAngleRange = asteroidFragmentRandomAngleRange;
            _fieldCalculationHelper = fieldCalculationHelper;
            _newAsteroidSpawnCoolDown = newAsteroidSpawnCoolDown;
            
            _buffer = new Collider2D[100];
            _newAsteroidSpawnTimeLeft = _newAsteroidSpawnCoolDown;
        }

        public override void Initialize(World world, Dispatcher messageDispatcher)
        {
            base.Initialize(world, messageDispatcher);
            MessageDispatcher.Subscribe(MessageType.SpawnAsteroidFragments, SpawnAsteroidFractions);
            MessageDispatcher.Subscribe(MessageType.AsteroidDestroyed, HandleAsteroidDestroyed);
        }

        public override void Process(Entity entity)
        {   
        }

        public override void PostProcess()
        {
            
            if (_asteroids.Count(asteroid => !asteroid.IsFraction) < _asteroidsInitSpawnAmount)
            {
                if (_newAsteroidSpawnTimeLeft <= 0)
                {
                    SpawnAsteroidRandomly();
                    _newAsteroidSpawnTimeLeft = _newAsteroidSpawnCoolDown;
                }
                else
                    _newAsteroidSpawnTimeLeft -= Time.deltaTime;
            }
        }

        public void Start()
        {
            if (_isEnabled) return;
            if (_isFirstStart)
            {
                for (var i = 0; i < _asteroidsInitSpawnAmount; i++)
                {
                    SpawnAsteroidRandomly();
                }
            }
            
            _isEnabled = true;
        }

        private void SpawnAsteroidRandomly()
        {
            Vector2 asteroidSpawnPosition = GetAsteroidRandomSpawnPosition();
            var asteroidRotation = MathHelper.GetRandom2DRotation();

            var asteroidVelocity = asteroidRotation * Vector3.up
                                                    * Random.Range(_asteroidVelocityRange.x, _asteroidVelocityRange.y);

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

            SpawnAsteroid(movableData, _asteroidPrefab, false);
        }

        public void Stop()
        {
            if (!_isEnabled) return;
            
            _isEnabled = false;
        }
        
        public void SpawnAsteroid(MovableData movableData, GameObject prefab, bool isFraction)
        {
            var asteroidGO = GameObject.Instantiate(prefab,
                movableData.position, movableData.rotation);
            var asteroid = World.CreateEntity(asteroidGO);
            var asteroidComponent = new AsteroidComponent(isFraction);
            asteroid.AddComponent(asteroidComponent);
            
            asteroid.AddComponent(new MovableComponent(movableData));
            
            var collisionDetector = asteroidGO.GetComponent<CollisionDetector2D>();
            var collisionDetectorComponent =
                (CollisionDetectorComponent)asteroid.AddComponent(new CollisionDetectorComponent());
            collisionDetectorComponent.SubscribeDetector(collisionDetector);
            
            _asteroids.Add(asteroidComponent);
        }

        private void SpawnAsteroidFractions(object positionObj)
        {
            Tuple<Vector3, Vector3> positionAndDirection = (Tuple<Vector3, Vector3>)positionObj;
            Vector3 rootPosition = positionAndDirection.Item1;

            var rootRotation = MathHelper.GetRandom2DRotation();

            var circleFraction = 369f / _asteroidFragmentsSpawnAmount;
            
            for (int i = 0; i < _asteroidFragmentsSpawnAmount; i++)
            {
                var positionOffset = Quaternion.Euler(0f, 0f, circleFraction * i) * (Vector3.up * 0.3f);
                
                positionOffset = rootPosition + rootRotation * positionOffset;

                var randomVelocity = Random.Range(_asteroidFragmentVelocityRange.x,
                    _asteroidFragmentVelocityRange.y) * positionAndDirection.Item2;
                randomVelocity = Quaternion.Euler(0f, 0f, Random.Range(_asteroidFragmentRandomAngleRange.x,
                    _asteroidFragmentRandomAngleRange.y)) * randomVelocity;
                    
                var movableData = new MovableData()
                {
                    accelerateForward = false,
                    acceleration = Vector2.zero,
                    destroyOutsideTheField = false,
                    friction = 0f,
                    position = positionOffset,
                    rotation = MathHelper.GetRandom2DRotation(),
                    velocity = randomVelocity
                };

                SpawnAsteroid(movableData, _asteroidFractionPrefab, true);
            }
        }

        private void HandleAsteroidDestroyed(object asteroid)
        {
            if (!_isEnabled) return;

            var asteroidComponent = (AsteroidComponent)asteroid;
            
            if (_asteroids.Count(asteroid => !asteroid.IsFraction) == _asteroidsInitSpawnAmount - 1)
                _newAsteroidSpawnTimeLeft = _newAsteroidSpawnCoolDown;
            
            _asteroids.Remove(asteroidComponent);
        }

        private Vector3 GetAsteroidRandomSpawnPosition()
        {
            Vector3 position;
            do
            {
                position = _fieldCalculationHelper.GetRandomPointFromInside();
            } while (Physics2D.OverlapCircleNonAlloc(position, OVERLAP_RADIUS, _buffer) != 0);

            return position;
        }
        public override void Destroy()
        {
            MessageDispatcher.Unsubscribe(MessageType.SpawnAsteroidFragments, SpawnAsteroidFractions);
            MessageDispatcher.Unsubscribe(MessageType.AsteroidDestroyed, HandleAsteroidDestroyed);
        }
    }
}