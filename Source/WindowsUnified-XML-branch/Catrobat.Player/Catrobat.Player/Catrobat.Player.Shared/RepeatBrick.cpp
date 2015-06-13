#include "pch.h"
#include "RepeatBrick.h"
#include "Interpreter.h"

using namespace std;

RepeatBrick::RepeatBrick(FormulaTree *times, std::shared_ptr<Script> parent) :
	ContainerBrick(TypeOfBrick::ContainerBrick, parent), m_timesToRepeat(times)
{
	m_brickList = new list<Brick*>();
}


RepeatBrick::~RepeatBrick(void)
{
	delete m_brickList;
}

void RepeatBrick::Execute()
{
	// Synchronously execute all subsequent blocks
	unsigned int i = 0;
	int global = 0;
	int times = Interpreter::Instance()->EvaluateFormulaToInt(m_timesToRepeat, GetParent()->GetParent());

	while (m_brickList->size() > 0 && global < times)
	{
		GetBrick(i)->Execute();
		i++;
		if (i >= m_brickList->size())
		{
			i = 0; // Reset counter
			global++;
		}

        Concurrency::wait(20);
	}
}

void RepeatBrick::AddBrick(Brick *brick)
{
	m_brickList->push_back(brick);
}

Brick *RepeatBrick::GetBrick(int index)
{
	list<Brick*>::iterator it = m_brickList->begin();
	advance(it, index);
	return *it;
}