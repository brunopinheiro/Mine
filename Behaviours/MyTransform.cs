using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Mine {
	
	public class MyTransform : MyBehaviour {
		
		#region position
		
		public Vector2 Position {
			get { return this.GetPosition(); }
			set { this.SetPosition(value); }
		}
		
		private Vector2 GetPosition() {
			float radius = this.GetPositionRadius();
			float angle = this.GetLookAtAngle();
			
			Vector2 parentScale = this.GetParentScale();
			Vector2 position = Vector2.Zero;
			position.X = (float)(radius * parentScale.X * Math.Cos(angle));
			position.Y = (float)(radius * parentScale.Y * Math.Sin(angle));
			
			Vector2 parentPosition = this.GetParentPosition();
			return parentPosition + position;
		}
		
		private void SetPosition(Vector2 position) {
			Vector2 parentPosition = this.GetParentPosition();
			this.localPosition = position - parentPosition;
		}
		
		private float GetPositionRadius() {
			double x = Math.Pow(this.localPosition.X,2);
			double y = Math.Pow(this.localPosition.Y,2);
			double radius = Math.Sqrt(x + y);
			return (float)radius;
		}
		
		private float GetLookAtAngle() {
			float angle = (float)Math.Atan2(this.localPosition.Y,this.localPosition.X);
			float parentRotation = this.GetParentRotation();
			return parentRotation + angle;
		}
		
		public Vector2 localPosition = Vector2.Zero;
		
		#endregion
		
		#region scale
		
		public Vector2 Scale {
			get { return this.GetScale(); }
			set { this.SetScale(value); }
		}
		
		private Vector2 GetScale() {
			Vector2 parentScale = this.GetParentScale();
			return this.localScale + parentScale;
		}
		
		private void SetScale(Vector2 scale) {
			Vector2 parentScale = this.GetParentScale();
			this.localScale = scale - parentScale;
		}
		
		public Vector2 localScale = Vector2.One;
		
		#endregion
		
		#region rotation
		
		public float Rotation {
			get { return this.GetRotation(); }
			set { this.SetRotation(value); }
		}
		
		private float GetRotation() {
			float parentRotation = this.GetParentRotation();
			return this.localRotation + parentRotation;
		}
		
		private void SetRotation(float rotation) {
			float parentRotation = this.GetParentRotation();
			this.localRotation = rotation - parentRotation;
		}
		
		public float localRotation = 0;
		
		#endregion
		
		#region heritage
		
		public MyTransform[] Children {
			get { return this.children.ToArray(); }
		}
		
		private Vector2 GetParentPosition() {
			Vector2 parentPosition = Vector2.Zero;
			if(this.parent != null) parentPosition = this.parent.Position;
			return parentPosition;
		}
		
		private Vector2 GetParentScale() {
			Vector2 parentScale = Vector2.Zero;
			if(this.parent != null) parentScale = this.parent.Scale;
			return parentScale;
		}
		
		private float GetParentRotation() {
			float parentRotation = 0;
			if(this.parent != null) parentRotation = this.parent.Rotation;
			return parentRotation;
		}
		
		public MyTransform Parent {
			get { return this.parent; }
			set {
				if(this.parent != null)	this.parent.RemoveChild(this);
				this.parent = value;
				if(this.parent != null) this.parent.AddChild(this);
			}
		}
		
		protected void AddChild(MyTransform child) {
			this.children.Add(child);
		}
		
		protected void RemoveChild(MyTransform child) {
			this.children.Remove(child);
		}
		
		private List<MyTransform> children = new List<MyTransform>();
		private MyTransform parent;
		
		#endregion
		
	}
	
}

