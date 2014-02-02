using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Mine {
    
    public class MyBoxCollider : MyCollider {

        #region collisions

        public static MyBoxCollider[] CircleCast(Vector2 position, float radius) {
            List<MyBoxCollider> collisions = new List<MyBoxCollider>();
            MyBoxCollider[] allBoundingBoxes = MyDirector.Instance.CurrentScene.Container.GetAllBehavioursInChildren<MyBoxCollider>();
            foreach(MyBoxCollider boundingBox in allBoundingBoxes) {
                BoundingSphere boundingSphere = new BoundingSphere(new Vector3(position.X,position.Y,0),radius);
                if(boundingBox.BoundingBox.Intersects(boundingSphere)) collisions.Add(boundingBox);
            }
            return collisions.ToArray();
        }

        public MyBoxCollider[] CircleCast(float radius) {
            return MyBoxCollider.CircleCast(this.Transform.Position,radius);
        }

        public override ContainmentType Contains(BoundingFrustum frustum) {
           return this.BoundingBox.Contains(frustum);
        }

        public override virtual ContainmentType Contains(BoundingSphere sphere) {
            return this.BoundingBox.Contains(sphere);
        }

        public override ContainmentType Contains(BoundingBox box) {
            return this.BoundingBox.Contains(box);
        }

        public override ContainmentType Contains(Vector3 point) {
            return this.BoundingBox.Contains(point);
        }

        public override bool Intersects(BoundingFrustum frustum) {
            return this.BoundingBox.Intersects(frustum);
        }

        public override bool Intersects(BoundingSphere sphere) {
            return this.BoundingBox.Intersects(sphere);
        }

        public override bool Intersects(BoundingBox box) {
            return this.BoundingBox.Intersects(box);
        }

        public override PlaneIntersectionType Intersects(Plane plane) {
            return this.BoundingBox.Intersects(plane);
        }

        public override float? Intersects(Ray ray) {
            return this.BoundingBox.Intersects(ray);
        }

        #endregion

        #region box

        public BoundingBox BoundingBox {
            get {
                if(this.autoFit && this.Actor.Renderer != null) return GetAutoBoundingBox();
                return this.boundingBox;
            }
        }

        private BoundingBox GetAutoBoundingBox() {
            Vector3 min = Vector3.Zero;
            min.X = this.Transform.Position.X - this.Actor.Renderer.Width * .5f;
            min.Y = this.Transform.Position.Y - this.Actor.Renderer.Height * .5f;
            Vector3 max = Vector3.Zero;
            max.X = this.Transform.Position.X + this.Actor.Renderer.Width * .5f;
            max.Y = this.Transform.Position.Y + this.Actor.Renderer.Height * .5f;
            return new BoundingBox(min,max);
        }

        public bool autoFit = true;
        private BoundingBox boundingBox;
        
        #endregion

    }

}
