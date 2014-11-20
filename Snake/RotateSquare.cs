using System;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Snake{
	public class RotateSquare:IGameObject{
		GraphicsState state;
		public float angle;
		public Point location;
		private Point direction;
		public RotateSquare(){
		
		
		}

		#region IGameObject implementation

		public void passData (GameData newInfo){
			direction.X += newInfo.point.X;
			direction.Y += newInfo.point.Y;
		}

		public void update (double gameTime){
			angle += 1;
			if(angle > 359){
				angle = 0;
			}
			location.X += direction.X;
			location.Y += direction.Y;

		}

		public void draw (System.Drawing.Graphics brush){
			setupTransform(brush);
			renderObject(brush);
			restoreTransform(brush);
		}

		public System.Drawing.Rectangle getRectangle (){
			throw new NotImplementedException();
		}

		#endregion

		void setupTransform (Graphics brush){
			state=brush.Save();		
			Matrix mx=new Matrix();
			mx.Rotate(angle,MatrixOrder.Append);
			mx.Translate(this.location.X,this.location.Y,MatrixOrder.Append);
			brush.Transform=mx;
		}

		void renderObject (Graphics brush){
		
			//Matrix 1
			Matrix mx = new Matrix();
			mx.Rotate(angle, MatrixOrder.Append);
			mx.Translate(this.location.X, this.location.Y, MatrixOrder.Append);
			brush.Transform = mx;
			brush.FillRectangle(Brushes.Black, -100, -100, 200, 200);
		}

		void restoreTransform (Graphics brush){
			brush.Restore(state);
		}
	}
}

