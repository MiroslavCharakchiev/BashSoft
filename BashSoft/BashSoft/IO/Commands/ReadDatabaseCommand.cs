﻿using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class ReadDatabaseCommand : Command
    {
        public ReadDatabaseCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length == 2)
            {
                var filename = Data[1];
                this.Repositiry.LoadData(filename);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }

        }
    }
}
