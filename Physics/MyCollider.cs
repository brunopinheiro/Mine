using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mine {
    
    public abstract class MyCollider : MyBehaviour {

        public abstract ContainmentType Contains(BoundingFrustum frustum);
        public abstract ContainmentType Contains(BoundingSphere sphere);
        public abstract ContainmentType Contains(BoundingBox box);
        public abstract ContainmentType Contains(Vector3 point);
        
        public abstract bool Intersects(BoundingFrustum frustum);
        public abstract bool Intersects(BoundingSphere sphere);
        public abstract bool Intersects(BoundingBox box);
        public abstract PlaneIntersectionType Intersects(Plane plane);
        public abstract float? Intersects(Ray ray);

    }

}
