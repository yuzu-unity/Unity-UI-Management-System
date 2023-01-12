using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace UnityFramework
{
    public interface ITabController
    {
        IReadOnlyReactiveProperty<int> State { get; }

        void SetState(int state);
    }
    
    public sealed class TabController : ITabController
    {
        public TabController(int length)
        {
            _length = length;
        }

        private readonly int _length;
        
        private readonly IntReactiveProperty _state = new IntReactiveProperty();
        public IReadOnlyReactiveProperty<int> State => _state;
        
        public void SetState(int state)
        {
            if (state < 0 || state >= _length)
            {
                throw new Exception($"state is out of range value:{state} length:{_length}");
            }
            _state.Value = state;
        }

    }
}