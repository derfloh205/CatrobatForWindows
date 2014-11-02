#pragma once

#include "Brick.h"

class MoveNStepsBrick :
	public Brick
{
public:
	MoveNStepsBrick(FormulaTree *steps, std::shared_ptr<Script> parent);
	virtual ~MoveNStepsBrick();
	void	Execute();

private:
	FormulaTree *m_steps;
	void CalculateNewCoordinates(float &x, float &y);
};
