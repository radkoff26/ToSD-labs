using Lab3.data;

namespace Lab3.scenario
{
    internal abstract class Scenario
    {
        protected Store store;
        protected RefreshCallback callback;

        protected Scenario(Store store, RefreshCallback callback)
        {
            this.store = store;
            this.callback = callback;
        }

        protected abstract void OnNewState(State state);

        public abstract void OnDraw(PaintEventArgs e, Form form);

        public virtual void OnCreate()
        {
            store.StateUpdate += OnNewState;
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
            
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {

        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {

        }

        public virtual void OnKeyUp(KeyEventArgs e, bool pressed)
        {

        }

        public virtual void OnKeyRight(KeyEventArgs e, bool pressed)
        {

        }

        public virtual void OnKeyDown(KeyEventArgs e, bool pressed)
        {

        }

        public virtual void OnKeyLeft(KeyEventArgs e, bool pressed)
        {

        }

        public virtual void OnKeyAdd(KeyEventArgs e, bool pressed)
        {

        }

        public virtual void OnKeySubtract(KeyEventArgs e, bool pressed)
        {

        }

        public virtual void OnKeySpace(KeyEventArgs e, bool pressed)
        {

        }

        public virtual void OnDestroy()
        {
            store.StateUpdate -= OnNewState;
        }
    }
}
