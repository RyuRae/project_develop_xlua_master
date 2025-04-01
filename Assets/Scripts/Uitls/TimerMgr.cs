using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Uitls
{
    class TimerNode
    {
        public TimerMgr.TimerHandler callback;

        /// <summary>
        /// 定时器触发的时间间隔
        /// </summary>
        public float duration;

        /// <summary>
        /// 第一次触发间隔时间
        /// </summary>
        public float delay;

        /// <summary>
        /// 重复次数
        /// </summary>
        public int repeat;

        /// <summary>
        /// 这个time过去的时间
        /// </summary>
        public float passedTime;

        /// <summary>
        /// 用户传的参数
        /// </summary>
        public object param;

        /// <summary>
        /// 是否已经删除
        /// </summary>
        public bool isRemoved;

        /// <summary>
        /// 表示这个timer的唯一ID号
        /// </summary>
        public int timerId;
    }

    public class TimerMgr : MonoSingleton<TimerMgr>
    {

        public delegate void TimerHandler(object param);

        private int autoIncId = 1;

        private Dictionary<int, TimerNode> timers = null;

        private List<TimerNode> removeTimers = null;

        private List<TimerNode> newAddTimers = null;

        private void Awake()
        {
            Init();
        }


        /// <summary>
        /// 初始化定时器
        /// </summary>
        private void Init()
        {
            this.timers = new Dictionary<int, TimerNode>();
            this.autoIncId = 1;

            this.removeTimers = new List<TimerNode>();

            this.newAddTimers = new List<TimerNode>();
        }


        private void Update()
        {
            float dt = Time.deltaTime;

            //把新加进来的放入表里
            for (int i = 0; i < this.newAddTimers.Count; i++)
            {
                this.timers.Add(this.newAddTimers[i].timerId, this.newAddTimers[i]);
            }
            this.newAddTimers.Clear();
            //end

            var it = this.timers.Values.GetEnumerator();
            while (it.MoveNext())
            {
                var node = it.Current;
                if (node.isRemoved)
                {
                    this.removeTimers.Add(node);
                    continue;
                }
                node.passedTime += dt;
                if (node.passedTime >= node.delay + node.duration)
                {
                    //做一次触发
                    node.callback(node.param);
                    node.repeat--;
                    node.passedTime -= (node.delay + node.duration);
                    node.delay = 0;//很重要
                    if (node.repeat == 0)
                    {
                        //触发次数结束，删除这个timer
                        node.isRemoved = true;
                        this.removeTimers.Add(node);
                    }
                    //end
                }
            }

            for (int i = 0; i < this.removeTimers.Count; i++)
            {
                this.timers.Remove(this.removeTimers[i].timerId);
            }
            this.removeTimers.Clear();
        }

        /// <summary>
        /// 执行一次
        /// </summary>
        /// <param name="callback">执行完成回调</param>
        /// <param name="delay">延迟执行时间</param>
        public int ScheduleOnce(TimerHandler callback, float delay)
        {

            return Schedule(callback, 1, 0, delay);
        }

        /// <summary>
        /// 执行一次
        /// </summary>
        /// <param name="callback">执行完成回调</param>
        /// <param name="delay">延迟执行时间</param>
        public int ScheduleOnce(TimerHandler callback, object param, float delay)
        {

            return Schedule(callback, param, 1, 0, delay);
        }

        /// <summary>
        /// 触发多次
        /// </summary>
        public int Schedule(TimerHandler callback, int repeat, float duration, float delay = 0.0f)
        {

            return Schedule(callback, null, repeat, duration, delay);
        }

        /// <summary>
        /// 触发多次
        /// </summary>
        public int Schedule(TimerHandler callback, object param, int repeat, float duration, float delay = 0.0f)
        {
            TimerNode timer = new TimerNode();
            timer.callback = callback;
            timer.param = param;
            timer.repeat = repeat;
            timer.duration = duration;
            timer.delay = delay;
            timer.isRemoved = false;
            timer.passedTime = timer.duration;
            timer.timerId = this.autoIncId;
            this.autoIncId++;
            //this.timers.Add(timer.timerId, timer);
            this.newAddTimers.Add(timer);

            return timer.timerId;
        }

        /// <summary>
        /// 取消定时器
        /// </summary>
        public void UnSchedule(int timerId)
        {
            if (!this.timers.ContainsKey(timerId))
            {
                return;
            }

            TimerNode node = this.timers[timerId];
            node.isRemoved = true;
        }


        /// <summary>
        /// 取消定时器
        /// </summary>
        public void UnSchedule(TimerHandler callback)
        {


        }

    }
}
