using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UISceneModule
{

    public enum UIType
    {
        DEFAULT = 0,
        LONGPRESS,
        DOUBLECLICK,
        DRAG,
        MOVE      
    }

    /**********************************************
    * Copyright (C) 2019 讯飞幻境（北京）科技有限公司
    * 模块名: UIComponent.cs
    * 创建者：RyuRae
    * 修改者列表：
    * 创建日期：
    * 功能描述：
    ***********************************************/
    public class UIComponent : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IMoveHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        #region Editor Properties
        public bool showFields;
        public bool showEvents;
        #endregion

        public UIType uiType = UIType.DEFAULT;

        private bool _longPressTrigger = false;

        private bool _IsBeginPress = false;

        [Header("长按事件的间隔计时")]
        public float _longPressTime = 1f;

        private float _curPointDownTime = 0f;

        [Serializable]
        public class OnMouseEvent : UnityEvent<UIComponent> { }

        [Serializable]
        public class OnDragEvent : UnityEvent<UIComponent, PointerEventData> { }

        /// <summary>按钮的悬浮事件</summary>
        public OnMouseEvent onHover = null;

        /// <summary>按钮的点击事件</summary>
        public OnMouseEvent onMouseClick = null;

        /// <summary>按钮的双击事件</summary>
        public OnMouseEvent onDoubleClick = null;

        /// <summary>按钮的选择事件</summary>
        public OnMouseEvent onSelect = null;

        /// <summary>按钮的长按事件</summary>
        public OnMouseEvent onLongPress = null;

        /// <summary>按钮的抬起事件</summary>
        public OnMouseEvent onRelease = null;

        /// <summary>按钮的离开事件</summary>
        public OnMouseEvent onMouseExit = null;

        /// <summary>物体的移动事件</summary>
        public OnMouseEvent onMove = null;

        /// <summary>物体的开始拖拽事件</summary>
        public OnDragEvent onBeginDrag = null;

        /// <summary>物体的拖拽事件</summary>
        public OnDragEvent onDrag = null;

        /// <summary>物体的结束拖拽事件</summary>
        public OnDragEvent onEndDrag = null;


        private void Update()
        {
            if (_IsBeginPress && !_longPressTrigger)
            {
                if (Time.time - _curPointDownTime > _longPressTime)
                {
                    _longPressTrigger = true;
                    //_IsBeginPress = false;
                    if (onLongPress != null)
                        onLongPress.Invoke(this);
                }
            }
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (onHover != null)
                onHover.Invoke(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _curPointDownTime = Time.time;
            _IsBeginPress = true;
            _longPressTrigger = false;
            //Debug.Log(_longPressTrigger);
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            _IsBeginPress = false;
            //_longPressTrigger = false;
            if (!_longPressTrigger) return;
            if (onRelease != null)
                onRelease.Invoke(this);
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            _IsBeginPress = false;
            //_longPressTrigger = false;
            if (onMouseExit != null)
                onMouseExit.Invoke(this);
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_longPressTrigger)
            {
                if (eventData.clickCount == 1)
                {
                    if (/*Input.GetMouseButtonUp(0) && */onMouseClick != null)
                        onMouseClick.Invoke(this);
                }
                else if (eventData.clickCount == 2)
                {
                    if (/*Input.GetMouseButtonUp(0) && */onDoubleClick != null)
                        onDoubleClick.Invoke(this);
                }
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (onSelect != null)
                onSelect.Invoke(this);
        }


        public void OnMove(AxisEventData eventData)
        {
            if (onMove != null)
                onMove.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (onBeginDrag != null)
            {
                onBeginDrag.Invoke(this, eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (onDrag != null)
            {
                onDrag.Invoke(this, eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (onEndDrag != null)
            {
                onEndDrag.Invoke(this, eventData);
            }
        }

    }
}
