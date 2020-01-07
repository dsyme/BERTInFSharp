﻿open System
open Modeling

type BertModelTester() = 
    /// Creates a random int32 tensor of the shape within the vocab size.
    static member ids_tensor(shape : int[], vocab_size : int, ?rng : Random, ?name : string) = 
        let rng = defaultArg rng (Random())
        let total_dims = shape |> Array.fold (fun x y -> x*y) 1
        match name with
        | Some(name) -> tf.constant(0, dtype=tf.int32,shape=shape,name=name)
        | _ -> tf.constant(0, dtype=tf.int32,shape=shape)


[<EntryPoint>]
let main argv =
    let batch_size = 2 // TODO check that a batch size of 1 does not cause issues
    let seq_length = 7
    let vocab_size = 99
    let input_ids = BertModelTester.ids_tensor([|batch_size; seq_length|], vocab_size)
    let config = {BertConfig.Default with vocab_size = Some(vocab_size)}
    let bertModel = BertModel(config, true, input_ids)
    tf.get_default_graph().get_operations() |> Seq.filter (fun x -> x.op.OpType = "VariableV2") |> Seq.iter (fun x -> printfn "%s" x.name)
    0 

