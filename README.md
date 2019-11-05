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

### Android Crash
![Android Crash](https://user-images.githubusercontent.com/13558917/68251544-3ba1f780-ffd8-11e9-85c2-386ef56467c3.gif)
 
