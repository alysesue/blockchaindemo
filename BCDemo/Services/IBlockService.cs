using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCDemo.Models;

namespace BCDemo.Services
{
    public interface IBlockService
    {
        void AddNextBlock(Block b);

        List<Block> GetBlockList();

    }
}
