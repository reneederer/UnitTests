module bhvvertraege_v7

open Expecto

[<Tests>]
let tests =
  testList "bhvvertraege_v7" [
    testCase
        "Jeder Klient muss einen Vertrag haben"
        (fun _ ->
           let actual = false
           let expected = true
           Expect.equal actual expected "Vertrag existiert nicht"
        )
  ]
