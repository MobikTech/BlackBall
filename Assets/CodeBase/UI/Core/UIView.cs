using System;
using System.Collections.Generic;
using System.Linq;
using BlackBall.Core;
using UnityEngine;

namespace BlackBall.UI.Core
{
    public abstract class UIView : CoreBehaviour
    {
        public event Action? HasOpened;
        public event Action? HasClosed;
        public abstract Type ActualType { get; }

        [SerializeField] private List<UIWidget>? _startWidgets;
        private List<UIWidget>? _childWidgets;
        protected IViewVisualizer _viewVisualizer = null!;


        internal virtual void Initialize(IViewVisualizer viewVisualizer)
        {
            _viewVisualizer = viewVisualizer;
            _childWidgets = GetComponentsInChildren<UIWidget>().ToList();
            _childWidgets.ForEach(widget => widget.Initialize());
            gameObject.SetActive(false);
        }
        internal virtual void Open<TOptions>(TOptions options) where TOptions : IOptions
        {
            gameObject.SetActive(true);
            _startWidgets?.ForEach(widget => widget.Visualize(IOptions.NoneOptions));
            HasOpened?.Invoke();
        }

        internal virtual void Close()
        {
            _childWidgets?
                .Where(widget => widget.IsActive)
                .ToList()
                .ForEach(widget => widget.Hide());
            gameObject.SetActive(false);
            HasClosed?.Invoke();
        }
    }
}
