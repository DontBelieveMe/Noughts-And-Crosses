using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace NoughtsAndCrosses
{
    public class EmptyState : State
    {
        public EmptyState() : base() { }
        protected override void Render(Graphics g)
        {
        }
    }

    public abstract class State
    {
        protected static List<State> states = new List<State>();
        private static State currentState = new EmptyState();

        protected int index = -1;

        public State()
        {
            index = states.Count;
        }

        public static void AddNew(State state)
        {
            states.Add(state);
        }

        public static void Start()
        {
            currentState = states[0];
        }

        protected void GotoNextState()
        {
            currentState.Reset();
            currentState = states[currentState.index + 1];
        }

        protected void GotoPreviousState()
        {
            // CRASH
            if (currentState.index == 0)
                return;

            currentState.Reset();
            currentState = states[currentState.index - 1];
        }

        protected void GotoFirstState()
        {
            currentState = states[0];
        }

        protected virtual void Tick() { }
        protected abstract void Render(Graphics g);
        protected virtual void OnClick(Point location) { }
        protected virtual void KeyPressed(Keys keys) { }
        protected T GetState<T>() { return states.OfType<T>().First(); }
        protected virtual void MouseMove(Point location) { }

        protected virtual void Reset() { }

        public static void Update() { currentState.Tick(); }
        public static void Draw(Graphics g) { currentState.Render(g); }
        public static void ProcessClick(Point location) { currentState.OnClick(location); }
        public static void ProcessKeyDown(Keys keys) { currentState.KeyPressed(keys); }
        public static void ProcessMouseMove(Point location) { currentState.MouseMove(location); }
        public static void ClearStack() { states.Clear(); currentState = new EmptyState(); }
    }
}
