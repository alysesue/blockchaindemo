using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCDemo.Models;
using System.Collections;
using Microsoft.AspNetCore.Http;

namespace BCDemo.Services
{
    public class BlockService : IBlockService
    {
        private List<Block> _blocks;
        private int Difficulty = 0; //your computer will be working pretty damn hard to find the hash that matches
        private int blockGenerationInterval = 10; //10 seconds
        private int difficultyAdjustmentInterval = 10; //10 blocks

        public BlockService()
        {
            _blocks = new List<Block>
            {
                new Block
                {
                    Index = 0,
                    CurrentHash = "0tUACuv4vofd3fUCYbntgiA==:DSAHZaATyFXEAYNrkUukhQ==",
                    PreviousHash = "0",
                    TimeStamp = new DateTime(2018, 02, 02, 12, 00, 00, 0000),
                    Data = "This is the genesis block",
                    Difficulty = Difficulty,
                    Nonce = 0
                },
            };
        }

        public void AddNextBlock(Block b)
        {
            b.Index = _blocks.Max(s => s.Index) + 1;
            b.CurrentHash = b.Difficulty + (HashHelper.CalculateHash(b.Index + b.PreviousHash + b.TimeStamp + b.Data + b.Nonce));
            b.PreviousHash = _blocks.Last().CurrentHash;
            b.TimeStamp = DateTime.Now;
            b.Data = b.Data;
            b.Difficulty = Difficulty;
            b.Nonce = 0;

        
            bool ValidateHashMatchesDiff(string currentHash)
            {
                currentHash = b.CurrentHash;
                int count = 0;

                for (int i = 0; i < currentHash.Length; i++)
                {
                    if (currentHash[i] == 0)
                    {
                        count++;
                    }
                    if (currentHash[i] != 0)
                    {
                        break;
                    }
                }

                if (count == Difficulty)
                {
                    return true;
                }
                return false;
            }


            while (!ValidateHashMatchesDiff(b.CurrentHash))
            {
                b.Nonce = b.Nonce++;
                b.TimeStamp = DateTime.Now;
                b.CurrentHash = b.Difficulty + (HashHelper.CalculateHash(b.Index + b.PreviousHash + b.TimeStamp + b.Data + b.Nonce));
            };

            _blocks.Add(b);
        }

        public List<Block> GetBlockList()
        {
            return _blocks;
        }

        public TimeSpan ActualTimeToGenerateBlocks()
        {
            List<Block> lastTenBlocks =
                _blocks.Skip(Math.Max(0, _blocks.Count() - blockGenerationInterval)).ToList<Block>();

            TimeSpan totalTimeTaken = lastTenBlocks.Last().TimeStamp - lastTenBlocks.First().TimeStamp;

            return totalTimeTaken;
        }

        public TimeSpan ExpectedTimeToGenerateBlocks()
        {
            int expectedTime = blockGenerationInterval * difficultyAdjustmentInterval; //10 blocks * 10 seconds = 100 seconds
            TimeSpan expectedTimeTS = TimeSpan.FromSeconds(expectedTime);

            return expectedTimeTS;
        }

        public int AdjustBlockDifficulty()
        {
            TimeSpan halfExpectedTimeTS = new TimeSpan(ExpectedTimeToGenerateBlocks().Minutes / 2);
            TimeSpan doubleExpectedTimeTS = new TimeSpan(ExpectedTimeToGenerateBlocks().Minutes * 2);

            if (ActualTimeToGenerateBlocks() < halfExpectedTimeTS)
            {
                return difficultyAdjustmentInterval + 1;
            }
            else if (ActualTimeToGenerateBlocks() > doubleExpectedTimeTS)
            {
                return difficultyAdjustmentInterval - 1;
            }
            else
            {
                return difficultyAdjustmentInterval;
            }
        }
    }
}
