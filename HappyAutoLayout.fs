// ------------------------------------------------------------------------
// HAPPY AUTO LAYOUT :)
// Terse, readable and safe auto-layout DSL for Xamarin iOS peeps.
// Well, that was the plan...
// (c) Ben Taylor 2014.  Available under MIT License.
// ------------------------------------------------------------------------

namespace HappyAutoLayout

open MonoTouch.UIKit

type ViewAttribute =
    | Left
    | Right
    | Top
    | Bottom
    | Leading
    | Trailing
    | Width
    | Height
    | CenterX
    | CenterY
    | Baseline

type AttributeRelationship =
    | Equal
    | GreaterOrEqual
    | LessOrEqual 

type Multiplier =
    | Times of float32
    | TimesZero
    | TimesOne
    with member __.Float = match __ with | Times(f) -> f | TimesOne -> 1.0f | TimesZero -> 0.0f

type Add =
    | Add of float32
    | AddZero
    with member __.Float = match __ with | Add(f) -> f | AddZero -> 0.0f

type Constraint =
    /// Constraint relating an attribute of the view to an attribute of a different view.  You can specify the multiplier and constant addition.
    | Relate of view1:UIView * view1Attribute:ViewAttribute * releationship:AttributeRelationship * view2:UIView * view2Attribute:ViewAttribute * multiplier:Multiplier * add:Add
    /// Constraint specifying that the view attribute must have the same value as the same attribute on the other view. 
    | Mirror of view1:UIView * attribute:ViewAttribute * view2:UIView
    /// Constraint setting the attribute value of a view directly without reference to another view.
    | Set of view:UIView * attribute:ViewAttribute * relationship:AttributeRelationship * value:float32

[<RequireQualifiedAccess>]
/// Module containing Hal functions for working with constraints.
module Hal = 

    /// <summary>Create constraints that pin the edges of view1 to the edges of view2.</summary>
    let edgeConstraints view1 view2 =
        [ Mirror(view1, Top, view2)
          Mirror(view1, Right, view2)
          Mirror(view1, Left, view2)
          Mirror(view1, Bottom, view2) ]

[<AutoOpen>]
module UIExtensions = 
    type UIView with 
        /// <summary>Add multiple constraints to this view or its sub-views.  Sets TranslatesAutoresizingMaskIntoConstraints to false on all views that a constraint targets.</summary>
        /// <param name="constraints">The list of constraints to be added to this view.</param>
        member __.AddConstraints(constraints:Constraint list) =
            let toNsAttr attr =
                match attr with
                | Left -> NSLayoutAttribute.Left
                | Right -> NSLayoutAttribute.Right
                | Top -> NSLayoutAttribute.Top
                | Bottom -> NSLayoutAttribute.Bottom
                | Leading -> NSLayoutAttribute.Leading
                | Trailing -> NSLayoutAttribute.Trailing
                | Width -> NSLayoutAttribute.Width
                | Height -> NSLayoutAttribute.Height
                | CenterX -> NSLayoutAttribute.CenterX
                | CenterY -> NSLayoutAttribute.CenterY
                | Baseline -> NSLayoutAttribute.Baseline

            let toNsRel rel = 
                match rel with
                | Equal -> NSLayoutRelation.Equal
                | GreaterOrEqual -> NSLayoutRelation.GreaterThanOrEqual
                | LessOrEqual -> NSLayoutRelation.LessThanOrEqual

            let constraints =
                [| for c in constraints do
                   let view, cons = 
                       match c with
                       | Relate(view1, attr1, rel, view2, attr2, mult, add) -> 
                           view1, NSLayoutConstraint.Create(view1, toNsAttr attr1, toNsRel rel, view2, toNsAttr attr2, mult.Float, add.Float)

                       | Mirror(view1, attr1, view2) ->
                           let attr = toNsAttr attr1
                           view1, NSLayoutConstraint.Create(view1, attr, NSLayoutRelation.Equal, view2, attr, 1.0f, 0.0f)

                       | Set(view, attr, rel, value) ->
                           view, NSLayoutConstraint.Create(view, toNsAttr attr, toNsRel rel, null, NSLayoutAttribute.NoAttribute, 0.0f, value)

                   // I did offer this as an option, but I can't currently see when a lib user would want it true.
                   view.TranslatesAutoresizingMaskIntoConstraints <- false

                   yield cons
                |]

            __.AddConstraints(constraints)

        /// <summary>Add a constraint relating to this view or sub-view.  Sets TranslatesAutoresizingMaskIntoConstraints to false on all views that a constraint targets.</summary>
        /// <param name="constraints">The constraint to be added to this view.</param>
        member __.AddConstraint(cons:Constraint) = __.AddConstraints([cons])


