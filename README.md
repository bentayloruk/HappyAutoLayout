HappyLayout
===========

**Terse, readable and safe auto-layout DSL for Xamarin iOS.**

Write your constraints like this:

```fsharp
mainView.AddConstraints(
  [ Mirror(happyView, Top, mainView)
    Relate(happyView, Left, Equal, mainView, Left, Times(0.8f), AddZero)
    Relate(happyView, Right, Equal, mainView, Right, Times(0.9f), Add(0.5f))
    Set(happyView, Bottom, Equal, 250.0f) ])
```    

Or this:

```fsharp
// Use the HappyLayout module function edgeConstraints, to create the 4 constraints I need.
mainView.AddConstraints(edgeConstraints happyView mainView)
```

## Constraint Types

There are three constraint types:

**Relate** *fully* expresses a constraint relationship between the properties of two views.

**Mirror** specifies that a view property should mirror the value of the same property on the second view.  *You could specify this with `Relate`, but `Mirror` is shorter and clearly expresses your intent.*

**Set** directly specifies the value of a property on the view.

## Dragon Information

HappyLayout is brought to you by Yak Shavers R Us.  I'm finding it useful, but would love to get feedback.  There may be dragons, but it's unlikely as the code is tiny.
