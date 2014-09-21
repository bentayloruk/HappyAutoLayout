HappyAutoLayout
===========

**Terse, readable and safe auto-layout F# DSL for Xamarin iOS.**

Write your constraints like this:

```FSharp
mainView.AddConstraints(
  [ Mirror(happyView, Top, mainView)
    Relate(happyView, Left, Equal, mainView, Left, Times(0.8f), AddZero)
    Relate(happyView, Right, Equal, mainView, Right, Times(0.5f), Add(10.0f))
    Set(happyView, Bottom, Equal, 250.0f) ])
```    

Or this:

```FSharp
// Use the HappyLayout module function edgeConstraints, to create the 4 constraints I need.
mainView.AddConstraints(Hal.edgeConstraints happyView mainView)
```

## Constraint Types

There are three constraint types:

**Relate** *fully* expresses a constraint relationship between the properties of two views.

**Mirror** specifies that a view property should mirror the value of the same property on the second view.  *You could specify this with `Relate`, but `Mirror` is shorter and clearly expresses your intent.*

**Set** directly specifies the value of a property on the view.

## Using HappyAutoLayout

Grab [HappyAutoLayout.fs](https://github.com/bentayloruk/HappyAutoLayout/blob/master/HappyAutoLayout.fs) and put it in your F# iOS project.

*HAL could be used from C# too.  Add an F# assembly project and this file and build away.  I might try it too one day, or you could just join us in F# land.  It's so warm and fuzzy here.*

## Contact

[@bentayloruk](https://twitter.com/bentayloruk)

## License

HappyAutoLayout is licensed under MIT.

## Dragon Information

HappyAutoLayout is at 0.1.  It is a tiny bit of code, so don't let that worry you.
