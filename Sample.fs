module Sample

open Expecto

[<Tests>]
let tests =
  testList "Sample" [
    testCase "uuuunivverse exists (╭ರᴥ•́)" <| fun _ ->
      let subject = true
      Expect.isTrue subject "I compute, therefore I am."

    //test "I am (should fail)" {
    //  "╰〳 ಠ 益 ಠೃ 〵╯" |> Expect.equal true false
    //}
  ]
