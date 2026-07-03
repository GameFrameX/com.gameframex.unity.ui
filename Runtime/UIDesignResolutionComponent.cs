using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// UI 设计分辨率配置组件。
    /// </summary>
    /// <remarks>
    /// Shared UI design resolution settings used by UGUI and FairyGUI adapters.
    /// </remarks>
    [Preserve]
    [DisallowMultipleComponent]
    [AddComponentMenu("GameFrameX/UI Design Resolution")]
    public sealed class UIDesignResolutionComponent : MonoBehaviour
    {
        /// <summary>
        /// UI 缩放模式。
        /// </summary>
        [Preserve]
        public enum UIScaleMode
        {
            ConstantPixelSize,
            ScaleWithScreenSize,
            ConstantPhysicalSize
        }

        /// <summary>
        /// 屏幕适配方式。
        /// </summary>
        [Preserve]
        public enum UIScreenMatchMode
        {
            MatchWidthOrHeight,
            MatchWidth,
            MatchHeight
        }

        [Preserve]
        [SerializeField] private UIScaleMode m_ScaleMode = UIScaleMode.ScaleWithScreenSize;

        [Preserve]
        [SerializeField] private UIScreenMatchMode m_ScreenMatchMode = UIScreenMatchMode.MatchWidthOrHeight;

        [Preserve]
        [SerializeField] private int m_DesignWidth = 1920;

        [Preserve]
        [SerializeField] private int m_DesignHeight = 1080;

        [Preserve]
        [Range(0f, 1f)]
        [SerializeField] private float m_MatchWidthOrHeight = 0.5f;

        [Preserve]
        [SerializeField] private float m_ConstantScaleFactor = 1f;

        [Preserve]
        [SerializeField] private float m_ReferencePixelsPerUnit = 100f;

        [Preserve]
        [SerializeField] private float m_FallbackScreenDPI = 96f;

        [Preserve]
        [SerializeField] private float m_DefaultSpriteDPI = 96f;

        [Preserve]
        [SerializeField] private bool m_IgnoreOrientation = false;

        /// <summary>
        /// 获取 UI 缩放模式。
        /// </summary>
        [Preserve]
        public UIScaleMode ScaleMode
        {
            get { return m_ScaleMode; }
            set { m_ScaleMode = value; }
        }

        /// <summary>
        /// 获取屏幕适配方式。
        /// </summary>
        [Preserve]
        public UIScreenMatchMode ScreenMatchMode
        {
            get { return m_ScreenMatchMode; }
            set { m_ScreenMatchMode = value; }
        }

        /// <summary>
        /// 获取设计分辨率宽度。
        /// </summary>
        [Preserve]
        public int DesignWidth
        {
            get { return Mathf.Max(1, m_DesignWidth); }
            set { m_DesignWidth = Mathf.Max(1, value); }
        }

        /// <summary>
        /// 获取设计分辨率高度。
        /// </summary>
        [Preserve]
        public int DesignHeight
        {
            get { return Mathf.Max(1, m_DesignHeight); }
            set { m_DesignHeight = Mathf.Max(1, value); }
        }

        /// <summary>
        /// 获取宽高混合适配权重。
        /// </summary>
        [Preserve]
        public float MatchWidthOrHeight
        {
            get { return Mathf.Clamp01(m_MatchWidthOrHeight); }
            set { m_MatchWidthOrHeight = Mathf.Clamp01(value); }
        }

        /// <summary>
        /// 获取固定像素缩放值。
        /// </summary>
        [Preserve]
        public float ConstantScaleFactor
        {
            get { return Mathf.Max(0.01f, m_ConstantScaleFactor); }
            set { m_ConstantScaleFactor = Mathf.Max(0.01f, value); }
        }

        /// <summary>
        /// 获取每单位参考像素。
        /// </summary>
        [Preserve]
        public float ReferencePixelsPerUnit
        {
            get { return Mathf.Max(1f, m_ReferencePixelsPerUnit); }
            set { m_ReferencePixelsPerUnit = Mathf.Max(1f, value); }
        }

        /// <summary>
        /// 获取备用屏幕 DPI。
        /// </summary>
        [Preserve]
        public float FallbackScreenDPI
        {
            get { return Mathf.Max(1f, m_FallbackScreenDPI); }
            set { m_FallbackScreenDPI = Mathf.Max(1f, value); }
        }

        /// <summary>
        /// 获取默认精灵 DPI。
        /// </summary>
        [Preserve]
        public float DefaultSpriteDPI
        {
            get { return Mathf.Max(1f, m_DefaultSpriteDPI); }
            set { m_DefaultSpriteDPI = Mathf.Max(1f, value); }
        }

        /// <summary>
        /// 获取是否忽略横竖屏方向。
        /// </summary>
        [Preserve]
        public bool IgnoreOrientation
        {
            get { return m_IgnoreOrientation; }
            set { m_IgnoreOrientation = value; }
        }
    }
}
