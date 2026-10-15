using System;
using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;
using NUnit.Framework;


namespace GameFrameX.UI.Tests
{
    internal sealed class UIOptionsUnitTests
    {
        [TearDown]
        public void TearDown()
        {
            GameFrameXOptions.Reset(); // 清除 Options 注册，避免跨用例静态泄漏
        }

        [Test]
        public void UIOptions_ConfigureAndTryGet_RoundTripsDiffs()
        {
            Func<UIFormHelperBase> formFactory = () => null;
            Func<UIGroupHelperBase> groupFactory = () => null;
            GameFrameXOptions.Reset();
            GameFrameXOptions.Configure(new UIOptions
            {
                EnableOpenUIFormSuccessEvent = false,
                EnableOpenUIFormFailureEvent = false,
                EnableOpenUIFormUpdateEvent = true,
                EnableOpenUIFormDependencyAssetEvent = true,
                EnableAutoReleaseUIForm = false,
                EnableCloseUIFormCompleteEvent = false,
                IsEnableUIShowAnimation = true,
                IsEnableUIHideAnimation = true,
                InstanceAutoReleaseInterval = 120f,
                InstanceCapacity = 32,
                InstanceExpireTime = 90f,
                InstancePriority = 5,
                UIFormHelperFactory = formFactory,
                UIGroupHelperFactory = groupFactory,
            });

            Assert.IsTrue(GameFrameXOptions.TryGet<UIOptions>(out var options));
            Assert.That(options.EnableOpenUIFormSuccessEvent, Is.False);
            Assert.That(options.EnableOpenUIFormFailureEvent, Is.False);
            Assert.That(options.EnableOpenUIFormUpdateEvent, Is.True);
            Assert.That(options.EnableOpenUIFormDependencyAssetEvent, Is.True);
            Assert.That(options.EnableAutoReleaseUIForm, Is.False);
            Assert.That(options.EnableCloseUIFormCompleteEvent, Is.False);
            Assert.That(options.IsEnableUIShowAnimation, Is.True);
            Assert.That(options.IsEnableUIHideAnimation, Is.True);
            Assert.That(options.InstanceAutoReleaseInterval, Is.EqualTo(120f));
            Assert.That(options.InstanceCapacity, Is.EqualTo(32));
            Assert.That(options.InstanceExpireTime, Is.EqualTo(90f));
            Assert.That(options.InstancePriority, Is.EqualTo(5));
            Assert.That(options.UIFormHelperFactory, Is.SameAs(formFactory));
            Assert.That(options.UIGroupHelperFactory, Is.SameAs(groupFactory));

            GameFrameXOptions.Reset();
            var defaults = new UIOptions();
            Assert.That(defaults.EnableOpenUIFormSuccessEvent, Is.True);
            Assert.That(defaults.EnableOpenUIFormFailureEvent, Is.True);
            Assert.That(defaults.EnableOpenUIFormUpdateEvent, Is.False);
            Assert.That(defaults.EnableOpenUIFormDependencyAssetEvent, Is.False);
            Assert.That(defaults.EnableAutoReleaseUIForm, Is.True);
            Assert.That(defaults.EnableCloseUIFormCompleteEvent, Is.True);
            Assert.That(defaults.IsEnableUIShowAnimation, Is.False);
            Assert.That(defaults.IsEnableUIHideAnimation, Is.False);
            Assert.That(defaults.InstanceAutoReleaseInterval, Is.EqualTo(60f));
            Assert.That(defaults.InstanceCapacity, Is.EqualTo(16));
            Assert.That(defaults.InstanceExpireTime, Is.EqualTo(60f));
            Assert.That(defaults.InstancePriority, Is.EqualTo(0));
            Assert.That(defaults.UIFormHelperFactory, Is.Null);
            Assert.That(defaults.UIGroupHelperFactory, Is.Null);
        }
    }
}
