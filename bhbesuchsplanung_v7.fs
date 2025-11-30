module Sample

open Expecto

[<Tests>]
let tests =
  testList "bhbesuchsplanung_v7" [
    testCase "uuuunivverse exists (╭ರᴥ•́)" <| fun _ ->
      let subject = true
      Expect.isTrue subject "I compute, therefore I am."
  ]
