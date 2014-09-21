HappyLayout
===========

Terse, readable and safe auto-layout DSL for Xamarin iOS.

Write your constraints like this:

```fsharp
mainView.AddConstraints(
  [ Mirror(happyView, Top, mainView)
    Mirror(happyView, Bottom, mainView)
    Relate(happyView, Left, Equal, mainView, Left, Times(0.8f), AddZero)
    Relate(happyView, Right, Equal, mainView, Right, Times(0.9f), Add(0.5f)) ])
```    
Or this:
```fsharp
mainView.AddConstraints(edgeConstraints happyView mainView)
```
