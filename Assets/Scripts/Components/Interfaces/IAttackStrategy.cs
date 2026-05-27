using System;

public interface IAttackStrategy
{
    void Execute();

    bool CanBeExecuted();
}
