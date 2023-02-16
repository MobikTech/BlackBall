using System;
using System.Collections.Generic;
using System.Linq;
using BlackBall.Core;
using UnityEngine;

namespace BlackBall.UI.Core
{
    public abstract class UISceneManager : CoreBehaviour, IViewVisualizer
    {
        [SerializeField] protected List<UIView>? _startViews;
        private Dictionary<Type, UIView> _allViews = null!;

        public TView Visualize<TView, TOptions>(TOptions options) where TView : UIView where TOptions : IOptions
        {
            TView view = GetView<TView>();
            view.Open(options);
            return view;
        }
        
        public TView Hide<TView>() where TView : UIView
        {
            TView view = GetView<TView>();
            view.Close();
            return view;
        }

        public TView GetView<TView>() where TView : UIView
        {
            if (!_allViews.ContainsKey(typeof(TView)))
                throw new Exception($"Such UI View({typeof(TView).Name}) doesn't exist in this scene");

            TView view = (TView)_allViews[typeof(TView)];
            return view;
        }

        private void Awake() => Initialize();

        protected virtual void Initialize()
        {
            gameObject.SetActive(true);
            _allViews = GetViewsDict();
            _allViews.Values.ToList().ForEach(view => view.Initialize(this));
            _startViews?.ForEach(view => view.Open(IOptions.NoneOptions));
        }

        private Dictionary<Type, UIView> GetViewsDict()
        {
            List<UIView> views = GetComponentsInChildren<UIView>(true).ToList();
            Dictionary<Type, UIView> result = new Dictionary<Type, UIView>();
            views.ForEach(view => result.Add(view.ActualType, view));
            return result;
        }
    }
}