# CollectionViewObservableCollectionRepro
 This a sample app to demonstrate that when `Xamarin.Forms.CollectionView.ItemSource` is an `ObservableCollection<T>`, it is not marshalling `ObservableCollection.CollectionChanged` to the MainThread.
 
This behavior has been confirmed on Xamarin.Forms v4.3.x and v4.4.0.936621-pre1.

This sample includes the following scenarios, demonstrating that the crash only happens when using a `CollectionView` with  `ObservableCollection<T>` and `ConfigureAwait(false)`:

| App Crashes? | `Xamarin.Forms.ItemsView` Type | `Xamarin.Forms.ItemsView.ItemSource` Type | `ConfigureAwait` | 
| ------- | ------------------------------ | ----------------------------------------- | ---------------- |
| ✅ No | `Xamarin.Forms.ListView` | `List<T>` | `true` | 
| ✅ No | `Xamarin.Forms.ListView` | `List<T>` | `false` | 
| ✅ No | `Xamarin.Forms.ListView` | `ObservableCollection<T>` | `true` | 
| ✅ No | `Xamarin.Forms.ListView` | `ObservableCollection<T>` | `false` | 
| ✅ No | `Xamarin.Forms.CollectionView` | `List<T>` | `true` | 
| ✅ No | `Xamarin.Forms.CollectionView` | `List<T>` | `false` | 
| ✅ No | `Xamarin.Forms.CollectionView` | `ObservableCollection<T>` | `true` | 
| 💥 Yes | `Xamarin.Forms.CollectionView` | `ObservableCollection<T>` | `false` | 

## Reproduction Steps

1. Clone this repo
2. Open `CollectionViewObservableCollectionRepro.sln` in Visual Studio
3. In Visual Studio, set `CollectionViewObservableCollectionRepro.iOS` as the Startup Project
4. In Visual Studio, build/deploy `CollectionViewObservableCollectionRepro.iOS` to an iOS Simulator or Device
5. Once the app launched, click on the red button
6. Confirm that the app crashes
7. In Visual Studio, set `CollectionViewObservableCollectionRepro.Android` as the Startup Project
8. In Visual Studio, build/deploy `CollectionViewObservableCollectionRepro.Android` to an Android Device or Emulator
9. Once the app launched, click on the red button
10. Confirm that the app crashes

### iOS Crash
![iOS Crash](https://user-images.githubusercontent.com/13558917/68251198-75bec980-ffd7-11e9-8622-6a045421a732.gif)

```bash
UIKit.UIKitThreadAccessException: UIKit Consistency error: you are calling a UIKit method that can only be invoked from the UI thread.
  at UIKit.UIApplication.EnsureUIThread () [0x0001a] in /Library/Frameworks/Xamarin.iOS.framework/Versions/13.6.0.12/src/Xamarin.iOS/UIKit/UIApplication.cs:95 
  at UIKit.UIViewController.get_IsViewLoaded () [0x00000] in /Library/Frameworks/Xamarin.iOS.framework/Versions/13.6.0.12/src/Xamarin.iOS/UIViewController.g.cs:2071 
  at Xamarin.Forms.Platform.iOS.ObservableItemsSource.NotLoadedYet () [0x00000] in D:\a\1\s\Xamarin.Forms.Platform.iOS\CollectionView\ObservableItemsSource.cs:139 
  at Xamarin.Forms.Platform.iOS.ObservableItemsSource.Add (System.Collections.Specialized.NotifyCollectionChangedEventArgs args) [0x0004c] in D:\a\1\s\Xamarin.Forms.Platform.iOS\CollectionView\ObservableItemsSource.cs:147 
  at Xamarin.Forms.Platform.iOS.ObservableItemsSource.CollectionChanged (System.Object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs args) [0x00023] in D:\a\1\s\Xamarin.Forms.Platform.iOS\CollectionView\ObservableItemsSource.cs:98 
  at System.Collections.ObjectModel.ObservableCollection`1[T].OnCollectionChanged (System.Collections.Specialized.NotifyCollectionChangedEventArgs e) [0x00018] in /Users/builder/jenkins/workspace/xamarin-macios/xamarin-macios/external/mono/external/corefx/src/System.ObjectModel/src/System/Collections/ObjectModel/ObservableCollection.cs:263 
  at System.Collections.ObjectModel.ObservableCollection`1[T].OnCollectionChanged (System.Collections.Specialized.NotifyCollectionChangedAction action, System.Object item, System.Int32 index) [0x00000] in /Users/builder/jenkins/workspace/xamarin-macios/xamarin-macios/external/mono/external/corefx/src/System.ObjectModel/src/System/Collections/ObjectModel/ObservableCollection.cs:338 
  at System.Collections.ObjectModel.ObservableCollection`1[T].InsertItem (System.Int32 index, T item) [0x0001a] in /Users/builder/jenkins/workspace/xamarin-macios/xamarin-macios/external/mono/external/corefx/src/System.ObjectModel/src/System/Collections/ObjectModel/ObservableCollection.cs:196 
  at System.Collections.ObjectModel.Collection`1[T].Add (T item) [0x00020] in /Users/builder/jenkins/workspace/xamarin-macios/xamarin-macios/external/mono/external/corefx/src/Common/src/CoreLib/System/Collections/ObjectModel/Collection.cs:71 
  at CollectionViewObservableCollectionRepro.ObservableCollectionViewModel.ExecutePullToRefreshCommand () [0x00083] in /Users/Shared/GitHub/CollectionViewObservableCollectionRepro/CollectionViewObservableCollectionRepro/ViewModels/ObservableCollectionViewModel.cs:23 
  at CollectionViewObservableCollectionRepro.ObservableCollectionViewModel.ExecutePullToRefreshCommand () [0x00196] in /Users/Shared/GitHub/CollectionViewObservableCollectionRepro/CollectionViewObservableCollectionRepro/ViewModels/ObservableCollectionViewModel.cs:21 
  at AsyncAwaitBestPractices.SafeFireAndForgetExtensions.HandleSafeFireAndForget[TException] (System.Threading.Tasks.Task task, System.Boolean continueOnCapturedContext, System.Action`1[T] onException) [0x00027] in /Users/bramin/GitHub/AsyncAwaitBestPractices/Src/AsyncAwaitBestPractices/SafeFireAndForgetExtensions.cs:61 
  at System.Runtime.CompilerServices.AsyncMethodBuilderCore+<>c.<ThrowAsync>b__7_0 (System.Object state) [0x00000] in /Users/builder/jenkins/workspace/xamarin-macios/xamarin-macios/external/mono/mcs/class/referencesource/mscorlib/system/runtime/compilerservices/AsyncMethodBuilder.cs:1021 
  at Foundation.NSAsyncSynchronizationContextDispatcher.Apply () [0x00000] in /Library/Frameworks/Xamarin.iOS.framework/Versions/13.6.0.12/src/Xamarin.iOS/Foundation/NSAction.cs:178 
--- End of stack trace from previous location where exception was thrown ---

  at (wrapper managed-to-native) UIKit.UIApplication.UIApplicationMain(int,string[],intptr,intptr)
  at UIKit.UIApplication.Main (System.String[] args, System.IntPtr principal, System.IntPtr delegate) [0x00005] in /Library/Frameworks/Xamarin.iOS.framework/Versions/13.6.0.12/src/Xamarin.iOS/UIKit/UIApplication.cs:86 
  at UIKit.UIApplication.Main (System.String[] args, System.String principalClassName, System.String delegateClassName) [0x0000e] in /Library/Frameworks/Xamarin.iOS.framework/Versions/13.6.0.12/src/Xamarin.iOS/UIKit/UIApplication.cs:65 
  at CollectionViewObservableCollectionRepro.iOS.Application.Main (System.String[] args) [0x00000] in /Users/Shared/GitHub/CollectionViewObservableCollectionRepro/CollectionViewObservableCollectionRepro.iOS/Main.cs:7
2019-11-05 14:41:40.095474-0800 CollectionViewObservableCollectionRepro.iOS[23981:450195] Unhandled managed exception: UIKit Consistency error: you are calling a UIKit method that can only be invoked from the UI thread. (UIKit.UIKitThreadAccessException)
  at UIKit.UIApplication.EnsureUIThread () [0x0001a] in /Library/Frameworks/Xamarin.iOS.framework/Versions/13.6.0.12/src/Xamarin.iOS/UIKit/UIApplication.cs:95 
  at UIKit.UIViewController.get_IsViewLoaded () [0x00000] in /Library/Frameworks/Xamarin.iOS.framework/Versions/13.6.0.12/src/Xamarin.iOS/UIViewController.g.cs:2071 
  at Xamarin.Forms.Platform.iOS.ObservableItemsSource.NotLoadedYet () [0x00000] in D:\a\1\s\Xamarin.Forms.Platform.iOS\CollectionView\ObservableItemsSource.cs:139 
  at Xamarin.Forms.Platform.iOS.ObservableItemsSource.Add (System.Collections.Specialized.NotifyCollectionChangedEventArgs args) [0x0004c] in D:\a\1\s\Xamarin.Forms.Platform.iOS\CollectionView\ObservableItemsSource.cs:147 
  at Xamarin.Forms.Platform.iOS.ObservableItemsSource.CollectionChanged (System.Object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs args) [0x00023] in D:\a\1\s\Xamarin.Forms.Platform.iOS\CollectionView\ObservableItemsSource.cs:98 
  at System.Collections.ObjectModel.ObservableCollection`1[T].OnCollectionChanged (System.Collections.Specialized.NotifyCollectionChangedEventArgs e) [0x00018] in /Users/builder/jenkins/workspace/xamarin-macios/xamarin-macios/external/mono/external/corefx/src/System.ObjectModel/src/System/Collections/ObjectModel/ObservableCollection.cs:263 
  at System.Collections.ObjectModel.ObservableCollection`1[T].OnCollectionChanged (System.Collections.Specialized.NotifyCollectionChangedAction action, System.Object item, System.Int32 index) [0x00000] in /Users/builder/jenkins/workspace/xamarin-macios/xamarin-macios/external/mono/external/corefx/src/System.ObjectModel/src/System/Collections/ObjectModel/ObservableCollection.cs:338 
  at System.Collections.ObjectModel.ObservableCollection`1[T].InsertItem (System.Int32 index, T item) [0x0001a] in /Users/builder/jenkins/workspace/xamarin-macios/xamarin-macios/external/mono/external/corefx/src/System.ObjectModel/src/System/Collections/ObjectModel/ObservableCollection.cs:196 
  at System.Collections.ObjectModel.Collection`1[T].Add (T item) [0x00020] in /Users/builder/jenkins/workspace/xamarin-macios/xamarin-macios/external/mono/external/corefx/src/Common/src/CoreLib/System/Collections/ObjectModel/Collection.cs:71 
  at CollectionViewObservableCollectionRepro.ObservableCollectionViewModel.ExecutePullToRefreshCommand () [0x00083] in /Users/Shared/GitHub/CollectionViewObservableCollectionRepro/CollectionViewObservableCollectionRepro/ViewModels/ObservableCollectionViewModel.cs:23 
  at CollectionViewObservableCollectionRepro.ObservableCollectionViewModel.ExecutePullToRefreshCommand () [0x00196] in /Users/Shared/GitHub/CollectionViewObservableCollectionRepro/CollectionViewObservableCollectionRepro/ViewModels/ObservableCollectionViewModel.cs:21 
  at AsyncAwaitBestPractices.SafeFireAndForgetExtensions.HandleSafeFireAndForget[TException] (System.Threading.Tasks.Task task, System.Boolean continueOnCapturedContext, System.Action`1[T] onException) [0x00027] in /Users/bramin/GitHub/AsyncAwaitBestPractices/Src/AsyncAwaitBestPractices/SafeFireAndForgetExtensions.cs:61 
  at System.Runtime.CompilerServices.AsyncMethodBuilderCore+<>c.<ThrowAsync>b__7_0 (System.Object state) [0x00000] in /Users/builder/jenkins/workspace/xamarin-macios/xamarin-macios/external/mono/mcs/class/referencesource/mscorlib/system/runtime/compilerservices/AsyncMethodBuilder.cs:1021 
  at Foundation.NSAsyncSynchronizationContextDispatcher.Apply () [0x00000] in /Library/Frameworks/Xamarin.iOS.framework/Versions/13.6.0.12/src/Xamarin.iOS/Foundation/NSAction.cs:178 
--- End of stack trace from previous location where exception was thrown ---

  at (wrapper managed-to-native) UIKit.UIApplication.UIApplicationMain(int,string[],intptr,intptr)
  at UIKit.UIApplication.Main (System.String[] args, System.IntPtr principal, System.IntPtr delegate) [0x00005] in /Library/Frameworks/Xamarin.iOS.framework/Versions/13.6.0.12/src/Xamarin.iOS/UIKit/UIApplication.cs:86 
  at UIKit.UIApplication.Main (System.String[] args, System.String principalClassName, System.String delegateClassName) [0x0000e] in /Library/Frameworks/Xamarin.iOS.framework/Versions/13.6.0.12/src/Xamarin.iOS/UIKit/UIApplication.cs:65 
  at CollectionViewObservableCollectionRepro.iOS.Application.Main (System.String[] args) [0x00000] in /Users/Shared/GitHub/CollectionViewObservableCollectionRepro/CollectionViewObservableCollectionRepro.iOS/Main.cs:7
```

### Android Crash
![Android Crash](https://user-images.githubusercontent.com/13558917/68251544-3ba1f780-ffd8-11e9-85c2-386ef56467c3.gif)

```bash
[MonoDroid] Android.Util.AndroidRuntimeException: Only the original thread that created a view hierarchy can touch its views.
[MonoDroid]   at Java.Interop.JniEnvironment+InstanceMethods.CallNonvirtualVoidMethod (Java.Interop.JniObjectReference instance, Java.Interop.JniObjectReference type, Java.Interop.JniMethodInfo method, Java.Interop.JniArgumentValue* args) [0x00089] in <42bcf67b56bc4c909c2a5edee682522b>:0 
[MonoDroid]   at Java.Interop.JniPeerMembers+JniInstanceMethods.InvokeNonvirtualVoidMethod (System.String encodedMember, Java.Interop.IJavaPeerable self, Java.Interop.JniArgumentValue* parameters) [0x0001f] in <42bcf67b56bc4c909c2a5edee682522b>:0 
[MonoDroid]   at Android.Support.V7.Widget.RecyclerView+Adapter.NotifyItemInserted (System.Int32 position) [0x00022] in <844c913cb32f4510879031be59eb1dce>:0 
[MonoDroid]   at Xamarin.Forms.Platform.Android.AdapterNotifier.NotifyItemInserted (Xamarin.Forms.Platform.Android.IItemsViewSource source, System.Int32 startIndex) [0x00000] in D:\a\1\s\Xamarin.Forms.Platform.Android\CollectionView\AdapterNotifier.cs:27 
[MonoDroid]   at Xamarin.Forms.Platform.Android.ObservableItemsSource.Add (System.Collections.Specialized.NotifyCollectionChangedEventArgs args) [0x00041] in D:\a\1\s\Xamarin.Forms.Platform.Android\CollectionView\ObservableItemsSource.cs:131 
[MonoDroid]   at Xamarin.Forms.Platform.Android.ObservableItemsSource.CollectionChanged (System.Object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs args) [0x00023] in D:\a\1\s\Xamarin.Forms.Platform.Android\CollectionView\ObservableItemsSource.cs:88 
[MonoDroid]   at System.Collections.ObjectModel.ObservableCollection`1[T].OnCollectionChanged (System.Collections.Specialized.NotifyCollectionChangedEventArgs e) [0x00018] in <9e820d206a8d4177b453df9c2fa8d1cc>:0 
[MonoDroid]   at System.Collections.ObjectModel.ObservableCollection`1[T].OnCollectionChanged (System.Collections.Specialized.NotifyCollectionChangedAction action, System.Object item, System.Int32 index) [0x00009] in <9e820d206a8d4177b453df9c2fa8d1cc>:0 
[MonoDroid]   at System.Collections.ObjectModel.ObservableCollection`1[T].InsertItem (System.Int32 index, T item) [0x0001a] in <9e820d206a8d4177b453df9c2fa8d1cc>:0 
[MonoDroid]   at System.Collections.ObjectModel.Collection`1[T].Add (T item) [0x00020] in <6de48997d0c0445dbea8d4d83492d8c6>:0 
[MonoDroid]   at CollectionViewObservableCollectionRepro.ObservableCollectionViewModel.ExecutePullToRefreshCommand () [0x00083] in /Users/Shared/GitHub/CollectionViewObservableCollectionRepro/CollectionViewObservableCollectionRepro/ViewModels/ObservableCollectionViewModel.cs:23 
[MonoDroid]   at CollectionViewObservableCollectionRepro.ObservableCollectionViewModel.ExecutePullToRefreshCommand () [0x00196] in /Users/Shared/GitHub/CollectionViewObservableCollectionRepro/CollectionViewObservableCollectionRepro/ViewModels/ObservableCollectionViewModel.cs:21 
[MonoDroid]   at AsyncAwaitBestPractices.SafeFireAndForgetExtensions.HandleSafeFireAndForget[TException] (System.Threading.Tasks.Task task, System.Boolean continueOnCapturedContext, System.Action`1[T] onException) [0x00027] in /Users/bramin/GitHub/AsyncAwaitBestPractices/Src/AsyncAwaitBestPractices/SafeFireAndForgetExtensions.cs:61 
[MonoDroid]   at System.Runtime.CompilerServices.AsyncMethodBuilderCore+<>c.<ThrowAsync>b__7_0 (System.Object state) [0x00000] in <6de48997d0c0445dbea8d4d83492d8c6>:0 
[MonoDroid]   at Android.App.SyncContext+<>c__DisplayClass2_0.<Post>b__0 () [0x00000] in <896e354e404b45e0a70dbcb853702039>:0 
[MonoDroid]   at Java.Lang.Thread+RunnableImplementor.Run () [0x00008] in <896e354e404b45e0a70dbcb853702039>:0 
[MonoDroid]   at Java.Lang.IRunnableInvoker.n_Run (System.IntPtr jnienv, System.IntPtr native__this) [0x00009] in <896e354e404b45e0a70dbcb853702039>:0 
[MonoDroid]   at (wrapper dynamic-method) Android.Runtime.DynamicMethodNameCounter.43(intptr,intptr)
[MonoDroid]   --- End of managed Android.Util.AndroidRuntimeException stack trace ---
[MonoDroid] android.view.ViewRootImpl$CalledFromWrongThreadException: Only the original thread that created a view hierarchy can touch its views.
[MonoDroid] 	at android.view.ViewRootImpl.checkThread(ViewRootImpl.java:8191)
[MonoDroid] 	at android.view.ViewRootImpl.requestLayout(ViewRootImpl.java:1420)
[MonoDroid] 	at android.view.View.requestLayout(View.java:24469)
[chatty] uid=10164(com.minnick.collectionviewobservablecollectionrepro) identical 4 lines
[MonoDroid] 	at android.view.View.requestLayout(View.java:24469)
[MonoDroid] 	at android.widget.RelativeLayout.requestLayout(RelativeLayout.java:380)
[MonoDroid] 	at android.view.View.requestLayout(View.java:24469)
[chatty] uid=10164(com.minnick.collectionviewobservablecollectionrepro) identical 4 lines
[MonoDroid] 	at android.view.View.requestLayout(View.java:24469)
[MonoDroid] 	at android.support.v7.widget.RecyclerView.requestLayout(RecyclerView.java:4202)
[MonoDroid] 	at android.support.v7.widget.RecyclerView$RecyclerViewDataObserver.triggerUpdateProcessor(RecyclerView.java:5327)
[MonoDroid] 	at android.support.v7.widget.RecyclerView$RecyclerViewDataObserver.onItemRangeInserted(RecyclerView.java:5302)
[MonoDroid] 	at android.support.v7.widget.RecyclerView$AdapterDataObservable.notifyItemRangeInserted(RecyclerView.java:12022)
[MonoDroid] 	at android.support.v7.widget.RecyclerView$Adapter.notifyItemInserted(RecyclerView.java:7180)
```

### Environment

```bash
=== Visual Studio Enterprise 2019 for Mac ===

Version 8.3.7 (build 1)
Installation UUID: 067e39ef-3649-4129-b463-1abe8ba78c8f
	GTK+ 2.24.23 (Raleigh theme)
	Xamarin.Mac 5.16.1.24 (d16-3 / 08809f5b)

	Package version: 604000208

=== Mono Framework MDK ===

Runtime:
	Mono 6.4.0.208 (2019-06/07c23f2ca43) (64-bit)
	Package version: 604000208

=== NuGet ===

Version: 5.3.0.6192

=== .NET Core SDK ===

SDK: /usr/local/share/dotnet/sdk/3.0.100/Sdks
SDK Versions:
	3.0.100
	3.0.100-rc1-014190
	3.0.100-preview9-014004
	3.0.100-preview8-013656
	3.0.100-preview7-012821
	3.0.100-preview6-012264
	2.2.402
	2.1.701
	2.1.700
	1.1.14
MSBuild SDKs: /Library/Frameworks/Mono.framework/Versions/6.4.0/lib/mono/msbuild/Current/bin/Sdks

=== .NET Core Runtime ===

Runtime: /usr/local/share/dotnet/dotnet
Runtime Versions:
	3.0.0
	3.0.0-rc1-19456-20
	3.0.0-preview9-19423-09
	3.0.0-preview7-27912-14
	3.0.0-preview6-27804-01
	2.2.7
	2.1.13
	2.1.12
	2.1.11
	1.1.13
	1.0.16

=== Xamarin.Profiler ===

Version: 1.6.12.29
Location: /Applications/Xamarin Profiler.app/Contents/MacOS/Xamarin Profiler

=== Updater ===

Version: 11

=== Apple Developer Tools ===

Xcode 11.2 (15526)
Build 11B52

=== Xamarin.Mac ===

Xamarin.Mac not installed. Can't find /Library/Frameworks/Xamarin.Mac.framework/Versions/Current/Version.

=== Xamarin.iOS ===

Version: 13.6.0.12 (Visual Studio Enterprise)
Hash: e3c2b406d
Branch: xcode11.2
Build date: 2019-11-01 00:12:08-0400

=== Xamarin Designer ===

Version: 16.3.0.256
Hash: 8a223bfd7
Branch: remotes/origin/d16-3
Build date: 2019-11-01 21:02:02 UTC

=== Xamarin.Android ===

Version: 10.0.6.2 (Visual Studio Enterprise)
Commit: xamarin-android/d16-3/c407838
Android SDK: /Users/bramin/Library/Developer/Xamarin/android-sdk-macosx
	Supported Android versions:
		None installed

SDK Tools Version: 26.1.1
SDK Platform Tools Version: 29.0.3
SDK Build Tools Version: 28.0.3

Build Information: 
Mono: mono/mono/2019-06@476d72b9e32
Java.Interop: xamarin/java.interop/d16-3@5836f58
LibZipSharp: grendello/LibZipSharp/d16-3@71f4a94
LibZip: nih-at/libzip/rel-1-5-1@b95cf3fd
ProGuard: xamarin/proguard/master@905836d
SQLite: xamarin/sqlite/3.27.1@8212a2d
Xamarin.Android Tools: xamarin/xamarin-android-tools/d16-3@cb41333

=== Microsoft Mobile OpenJDK ===

Java SDK: /Users/bramin/Library/Developer/Xamarin/jdk/microsoft_dist_openjdk_1.8.0.25
1.8.0-25
Android Designer EPL code available here:
https://github.com/xamarin/AndroidDesigner.EPL

=== Android SDK Manager ===

Version: 1.4.0.65
Hash: c33b107
Branch: remotes/origin/d16-3
Build date: 2019-10-10 12:15:44 UTC

=== Android Device Manager ===

Version: 1.2.0.116
Hash: d2b2af0
Branch: remotes/origin/d16-3
Build date: 2019-10-10 12:16:06 UTC

=== Xamarin Inspector ===

Version: 1.4.3
Hash: db27525
Branch: 1.4-release
Build date: Mon, 09 Jul 2018 21:20:18 GMT
Client compatibility: 1

=== Build Information ===

Release ID: 803070001
Git revision: 9245c93bbca71766f0150b84e30dd42c394bd259
Build date: 2019-11-04 21:08:08+00
Build branch: release-8.3
Xamarin extensions: ea50d7eae9677fbbd7ff7578a7d1429b9946a728

=== Operating System ===

Mac OS X 10.15.1
Darwin 19.0.0 Darwin Kernel Version 19.0.0
    Thu Oct 17 16:17:15 PDT 2019
    root:xnu-6153.41.3~29/RELEASE_X86_64 x86_64
```
