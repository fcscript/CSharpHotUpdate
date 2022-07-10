#pragma once

class FFCTestInterface
{
public:
	static FFCTestInterface *Get();
public:
	float  HP;
	float  GetHP() const;	
};