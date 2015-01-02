namespace MoneyFlow.Seed

open System

module DataProvider =

    let _rnd = Random();

    let rnd64 max =
        let ldw = int32 (max >>> 0x20)
        let rdw : byte[] = Array.zeroCreate 4
        
        _rnd.NextBytes(rdw)

        let a = _rnd.Next(ldw)
        let b = BitConverter.ToUInt32(rdw, 0)

        ((uint64 a) <<< 0x20) ||| (uint64 b)

    let GetDate min max =
        let dt str =
            DateTime.Parse(str)

        let _min = dt(min)
        let _max = dt(max)

        let mutable dd = 
            uint64 (_max - _min).Ticks

        dd <- rnd64 dd

        _min.AddTicks(int64 dd)