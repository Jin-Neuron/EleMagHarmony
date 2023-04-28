/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * @file           : main.c
  * @brief          : Main program body
  ******************************************************************************
  * @attention
  *
  * Copyright (c) 2023 STMicroelectronics.
  * All rights reserved.
  *
  * This software is licensed under terms that can be found in the LICENSE file
  * in the root directory of this software component.
  * If no LICENSE file comes with this software, it is provided AS-IS.
  *
  ******************************************************************************
  */
/* USER CODE END Header */
/* Includes ------------------------------------------------------------------*/
#include "main.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */
#include <math.h>
#include <stdlib.h>
/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */

/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */
#define TimerNum 8
#define FloppyNum 3
#define RelayNum 4
/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
TIM_HandleTypeDef htim1;
TIM_HandleTypeDef htim6;
TIM_HandleTypeDef htim7;
TIM_HandleTypeDef htim17;

UART_HandleTypeDef huart2;

/* USER CODE BEGIN PV */
GPIO_PinState currentState[FloppyNum] ={ GPIO_PIN_RESET};
uint8_t currentPosition[FloppyNum] = {0};

char data[256];
uint8_t uartCnt = 0;
uint16_t notes[10] = {0};
uint16_t freqs[10] = {0};
uint16_t noteParFreq[127] = {0};
double tmp[10] = {0};

const double timer_clock = 64e6;
const uint8_t timerPeriod = 100;

uint8_t arrayLen = 0;
GPIO_TypeDef* relayPort[RelayNum] = {};
uint32_t relayPin[RelayNum] = {};
GPIO_TypeDef* floppyPort[FloppyNum * 2] = {};
uint32_t floppyPin[FloppyNum * 2] = {};
TIM_HandleTypeDef times[TimerNum] = {};
/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_USART2_UART_Init(void);
static void MX_TIM1_Init(void);
static void MX_TIM6_Init(void);
static void MX_TIM7_Init(void);
static void MX_TIM17_Init(void);
/* USER CODE BEGIN PFP */

/* USER CODE END PFP */

/* Private user code ---------------------------------------------------------*/
/* USER CODE BEGIN 0 */
//reset position of floppy
void resetStep(){
	for(uint8_t i = 0; i < FloppyNum * 2; i+=2){
		//i : Direction, i+1 : Step
		HAL_GPIO_WritePin(floppyPort[i], floppyPin[i], GPIO_PIN_SET);
		for(uint8_t j = 0; j < 79; j++){
			HAL_GPIO_WritePin(floppyPort[i+1], floppyPin[i+1], GPIO_PIN_SET);
			HAL_GPIO_WritePin(floppyPort[i+1], floppyPin[i+1], GPIO_PIN_RESET);
			HAL_Delay(10);
		}
		currentPosition[i / 2] = 0;
		currentState[i / 2] = GPIO_PIN_RESET;
		HAL_GPIO_WritePin(floppyPort[i], floppyPin[i], GPIO_PIN_RESET);
	}
}

//Timer interrupt (floppy control only)
void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim)
{
	uint8_t timerIdx = 0;
	for(; timerIdx < TimerNum; timerIdx++){
		if(htim->Instance == times[timerIdx].Instance)
			break;
	}
	if(timerIdx < TimerNum){
		//Update position
		//timer : 5 --> floppy : 0, 1
		//timer : 6 --> floppy : 2, 3
		//timer : 7 --> floppy : 4, 5
		uint8_t floppyIdx = timerIdx - 5;
		currentPosition[floppyIdx] = (currentState[floppyIdx] == GPIO_PIN_SET) ? currentPosition[floppyIdx] - 1 : currentPosition[floppyIdx] + 1;
		if(currentPosition[floppyIdx] <= 0){
			currentState[floppyIdx] = GPIO_PIN_RESET;
			HAL_GPIO_WritePin(floppyPort[floppyIdx * 2], floppyPin[floppyIdx * 2], currentState[floppyIdx]);
		}else if(currentPosition[floppyIdx] >= 158){
			currentState[floppyIdx] = GPIO_PIN_SET;
			HAL_GPIO_WritePin(floppyPort[floppyIdx * 2], floppyPin[floppyIdx * 2], currentState[floppyIdx]);
		}
		HAL_GPIO_TogglePin(floppyPort[floppyIdx * 2 + 1], floppyPin[floppyIdx * 2 + 1]);
	}
}

//Set frequency of Timer
void setTimer(uint8_t part, TIM_HandleTypeDef htim, uint32_t prescaler, uint32_t pwm){

  if(pwm > 99) pwm = 99;
  if(prescaler < 0) prescaler = 799;

  htim.Instance = htim.Instance;
  htim.Init.Prescaler = prescaler;
  htim.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim.Init.Period = timerPeriod - 1;	//because of start with 0
  htim.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_ENABLE;
  HAL_TIM_Base_Init(&htim);

	if(part < 1){
	  TIM_OC_InitTypeDef sConfigOC = {0};	//pwm setting only

	  sConfigOC.OCMode = TIM_OCMODE_PWM1;
	  sConfigOC.Pulse = pwm;
	  //set duty
	  sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
	  sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
	  sConfigOC.OCIdleState = TIM_OCIDLESTATE_RESET;
	  sConfigOC.OCNIdleState = TIM_OCNIDLESTATE_RESET;
	  HAL_TIM_PWM_ConfigChannel(&htim, &sConfigOC, TIM_CHANNEL_1);

	  //start
	  HAL_TIM_PWM_Start(&htim, TIM_CHANNEL_1);


	  sConfigOC.OCMode = TIM_OCMODE_TOGGLE;
	  sConfigOC.Pulse = 0;
	  sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
	  sConfigOC.OCNPolarity = TIM_OCNPOLARITY_HIGH;
	  sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
	  sConfigOC.OCIdleState = TIM_OCIDLESTATE_RESET;
	  sConfigOC.OCNIdleState = TIM_OCNIDLESTATE_RESET;

	  HAL_TIM_OC_ConfigChannel(&htim, &sConfigOC, TIM_CHANNEL_2);
	  HAL_TIM_OC_Start(&htim, TIM_CHANNEL_2);
	  HAL_TIMEx_OCN_Start(&htim, TIM_CHANNEL_2);

	  sConfigOC.Pulse = timerPeriod / 2 - 1;
	  HAL_TIM_OC_ConfigChannel(&htim, &sConfigOC, TIM_CHANNEL_3);
	  HAL_TIM_OC_Start(&htim, TIM_CHANNEL_3);
	  HAL_TIMEx_OCN_Start(&htim, TIM_CHANNEL_3);

	}else{
	  // start
	  HAL_TIM_Base_Start_IT(&htim);
	}

}

//Convert note number to frequency
float NoteConvert(uint16_t noteNum){
	int64_t tmp = noteNum - 69;
	double exp = (double)tmp / 12.0;
	double freq = 440 * pow(2.0, exp);
	return (float)freq;
}

//Serial interrupt
void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart)
{
	static uint8_t event = 0;
	static uint8_t part = 0;

	if(uartCnt){
		notes[part] = data[0];
		freqs[part] = noteParFreq[notes[part]];
		if(part < TimerNum){
			//double the frequency if it is floppy
			if(part >= 5 || part < 1){
					freqs[part] *= 2;
				setTimer(part, times[part], (timer_clock / (freqs[part] * timerPeriod) - 1),70);
			}else if(part < 1){
				setTimer(part, times[part], (timer_clock / (freqs[part] * timerPeriod) - 1),70);
			}else{
				notes[part] = 0;
				HAL_GPIO_TogglePin(relayPort[part - 1], relayPin[part - 1]);
			}
		}
		uartCnt = 0;
	}else{
		event = data[0] & 0xf0;
		part = data[0] & 0x0f;
		if(event){
			uartCnt++;
		}else{
			notes[part] = 0;
			freqs[part] = 0;
			if(part < TimerNum){
				if(part < 1){
					HAL_TIM_PWM_Stop(&times[part], TIM_CHANNEL_1);
					HAL_TIM_OC_Stop(&times[part], TIM_CHANNEL_2);	//stp_motor only
					HAL_TIMEx_OCN_Stop(&times[part], TIM_CHANNEL_2);
					HAL_TIM_OC_Stop(&times[part], TIM_CHANNEL_3);	//stp_motor only
					HAL_TIMEx_OCN_Stop(&times[part], TIM_CHANNEL_3);
				}else if(part > 4){
					HAL_TIM_Base_Stop_IT(&times[part]);
				}
			}
		}
	}
	HAL_UART_Receive_IT(&huart2, (uint8_t *)data, 1);
}


/* USER CODE END 0 */

/**
  * @brief  The application entry point.
  * @retval int
  */
int main(void)
{
  /* USER CODE BEGIN 1 */

  /* USER CODE END 1 */

  /* MCU Configuration--------------------------------------------------------*/

  /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
  HAL_Init();

  /* USER CODE BEGIN Init */

  /* USER CODE END Init */

  /* Configure the system clock */
  SystemClock_Config();

  /* USER CODE BEGIN SysInit */

  /* USER CODE END SysInit */

  /* Initialize all configured peripherals */
  MX_GPIO_Init();
  MX_USART2_UART_Init();
  MX_TIM1_Init();
  MX_TIM6_Init();
  MX_TIM7_Init();
  MX_TIM17_Init();
  /* USER CODE BEGIN 2 */

  //floppy1 --> 0 : direction, 1 : step
  //floppy2 --> 2 : direction, 3 : step
  //floppy3 --> 4 : direction, 5 : step
  floppyPort[0] = Direction1_GPIO_Port;
  floppyPort[1] = Step1_GPIO_Port;
  floppyPort[2] = Direction2_GPIO_Port;
  floppyPort[3] = Step2_GPIO_Port;
  floppyPort[4] = Direction3_GPIO_Port;
  floppyPort[5] = Step3_GPIO_Port;
  floppyPin[0] = Direction1_Pin;
  floppyPin[1] = Step1_Pin;
  floppyPin[2] = Direction2_Pin;
  floppyPin[3] = Step2_Pin;
  floppyPin[4] = Direction3_Pin;
  floppyPin[5] = Step3_Pin;
  relayPort[0] = Relay1_GPIO_Port;
  relayPort[1] = Relay2_GPIO_Port;
  relayPort[2] = Relay3_GPIO_Port;
  relayPort[3] = Relay4_GPIO_Port;
  relayPin[0] = Relay1_Pin;
  relayPin[1] = Relay2_Pin;
  relayPin[2] = Relay3_Pin;
  relayPin[3] = Relay4_Pin;

  for(uint8_t i = 0; i < RelayNum; i++){
	  HAL_GPIO_WritePin(relayPort[i], relayPin[i], GPIO_PIN_RESET);
  }
  for(uint8_t i = 0; i < FloppyNum * 2; i++){
	  HAL_GPIO_WritePin(floppyPort[i], floppyPin[i], GPIO_PIN_RESET);
  }

  //Melody by Solenoid and Stp_Motor
  times[0] = htim1;
  //Base by FloppyDrive
  times[5] = htim6;
  times[6] = htim7;
  times[7] = htim17;

  //reset floppy
  resetStep();
  //start UART interrupt
  HAL_UART_Receive_IT(&huart2, (uint8_t *)data, 1);
  for(uint8_t i = 0; i < 127; i++){
  	noteParFreq[i] =  (uint16_t)NoteConvert(i);
  }
  /* USER CODE END 2 */

  /* Infinite loop */
  /* USER CODE BEGIN WHILE */
  while (1)
  {
    /* USER CODE END WHILE */

    /* USER CODE BEGIN 3 */
  }
  /* USER CODE END 3 */
}

/**
  * @brief System Clock Configuration
  * @retval None
  */
void SystemClock_Config(void)
{
  RCC_OscInitTypeDef RCC_OscInitStruct = {0};
  RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};
  RCC_PeriphCLKInitTypeDef PeriphClkInit = {0};

  /** Initializes the RCC Oscillators according to the specified parameters
  * in the RCC_OscInitTypeDef structure.
  */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSI;
  RCC_OscInitStruct.HSIState = RCC_HSI_ON;
  RCC_OscInitStruct.HSICalibrationValue = RCC_HSICALIBRATION_DEFAULT;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSI;
  RCC_OscInitStruct.PLL.PLLMUL = RCC_PLL_MUL16;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    Error_Handler();
  }

  /** Initializes the CPU, AHB and APB buses clocks
  */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV1;

  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_2) != HAL_OK)
  {
    Error_Handler();
  }
  PeriphClkInit.PeriphClockSelection = RCC_PERIPHCLK_TIM1;
  PeriphClkInit.Tim1ClockSelection = RCC_TIM1CLK_HCLK;
  if (HAL_RCCEx_PeriphCLKConfig(&PeriphClkInit) != HAL_OK)
  {
    Error_Handler();
  }
}

/**
  * @brief TIM1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM1_Init(void)
{

  /* USER CODE BEGIN TIM1_Init 0 */

  /* USER CODE END TIM1_Init 0 */

  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};
  TIM_OC_InitTypeDef sConfigOC = {0};
  TIM_BreakDeadTimeConfigTypeDef sBreakDeadTimeConfig = {0};

  /* USER CODE BEGIN TIM1_Init 1 */

  /* USER CODE END TIM1_Init 1 */
  htim1.Instance = TIM1;
  htim1.Init.Prescaler = 799;
  htim1.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim1.Init.Period = 99;
  htim1.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim1.Init.RepetitionCounter = 0;
  htim1.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim1) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim1, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_PWM_Init(&htim1) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_OC_Init(&htim1) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterOutputTrigger2 = TIM_TRGO2_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim1, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sConfigOC.OCMode = TIM_OCMODE_PWM1;
  sConfigOC.Pulse = 0;
  sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
  sConfigOC.OCNPolarity = TIM_OCNPOLARITY_HIGH;
  sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
  sConfigOC.OCIdleState = TIM_OCIDLESTATE_RESET;
  sConfigOC.OCNIdleState = TIM_OCNIDLESTATE_RESET;
  if (HAL_TIM_PWM_ConfigChannel(&htim1, &sConfigOC, TIM_CHANNEL_1) != HAL_OK)
  {
    Error_Handler();
  }
  sConfigOC.OCMode = TIM_OCMODE_TIMING;
  if (HAL_TIM_OC_ConfigChannel(&htim1, &sConfigOC, TIM_CHANNEL_2) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_OC_ConfigChannel(&htim1, &sConfigOC, TIM_CHANNEL_3) != HAL_OK)
  {
    Error_Handler();
  }
  sBreakDeadTimeConfig.OffStateRunMode = TIM_OSSR_DISABLE;
  sBreakDeadTimeConfig.OffStateIDLEMode = TIM_OSSI_DISABLE;
  sBreakDeadTimeConfig.LockLevel = TIM_LOCKLEVEL_OFF;
  sBreakDeadTimeConfig.DeadTime = 0;
  sBreakDeadTimeConfig.BreakState = TIM_BREAK_DISABLE;
  sBreakDeadTimeConfig.BreakPolarity = TIM_BREAKPOLARITY_HIGH;
  sBreakDeadTimeConfig.BreakFilter = 0;
  sBreakDeadTimeConfig.Break2State = TIM_BREAK2_DISABLE;
  sBreakDeadTimeConfig.Break2Polarity = TIM_BREAK2POLARITY_HIGH;
  sBreakDeadTimeConfig.Break2Filter = 0;
  sBreakDeadTimeConfig.AutomaticOutput = TIM_AUTOMATICOUTPUT_DISABLE;
  if (HAL_TIMEx_ConfigBreakDeadTime(&htim1, &sBreakDeadTimeConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM1_Init 2 */

  /* USER CODE END TIM1_Init 2 */
  HAL_TIM_MspPostInit(&htim1);

}

/**
  * @brief TIM6 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM6_Init(void)
{

  /* USER CODE BEGIN TIM6_Init 0 */

  /* USER CODE END TIM6_Init 0 */

  TIM_MasterConfigTypeDef sMasterConfig = {0};

  /* USER CODE BEGIN TIM6_Init 1 */

  /* USER CODE END TIM6_Init 1 */
  htim6.Instance = TIM6;
  htim6.Init.Prescaler = 799;
  htim6.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim6.Init.Period = 99;
  htim6.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_ENABLE;
  if (HAL_TIM_Base_Init(&htim6) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim6, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM6_Init 2 */

  /* USER CODE END TIM6_Init 2 */

}

/**
  * @brief TIM7 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM7_Init(void)
{

  /* USER CODE BEGIN TIM7_Init 0 */

  /* USER CODE END TIM7_Init 0 */

  TIM_MasterConfigTypeDef sMasterConfig = {0};

  /* USER CODE BEGIN TIM7_Init 1 */

  /* USER CODE END TIM7_Init 1 */
  htim7.Instance = TIM7;
  htim7.Init.Prescaler = 799;
  htim7.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim7.Init.Period = 99;
  htim7.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_ENABLE;
  if (HAL_TIM_Base_Init(&htim7) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim7, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM7_Init 2 */

  /* USER CODE END TIM7_Init 2 */

}

/**
  * @brief TIM17 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM17_Init(void)
{

  /* USER CODE BEGIN TIM17_Init 0 */

  /* USER CODE END TIM17_Init 0 */

  /* USER CODE BEGIN TIM17_Init 1 */

  /* USER CODE END TIM17_Init 1 */
  htim17.Instance = TIM17;
  htim17.Init.Prescaler = 799;
  htim17.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim17.Init.Period = 99;
  htim17.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim17.Init.RepetitionCounter = 0;
  htim17.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_ENABLE;
  if (HAL_TIM_Base_Init(&htim17) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM17_Init 2 */

  /* USER CODE END TIM17_Init 2 */

}

/**
  * @brief USART2 Initialization Function
  * @param None
  * @retval None
  */
static void MX_USART2_UART_Init(void)
{

  /* USER CODE BEGIN USART2_Init 0 */

  /* USER CODE END USART2_Init 0 */

  /* USER CODE BEGIN USART2_Init 1 */

  /* USER CODE END USART2_Init 1 */
  huart2.Instance = USART2;
  huart2.Init.BaudRate = 422400;
  huart2.Init.WordLength = UART_WORDLENGTH_8B;
  huart2.Init.StopBits = UART_STOPBITS_1;
  huart2.Init.Parity = UART_PARITY_NONE;
  huart2.Init.Mode = UART_MODE_TX_RX;
  huart2.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart2.Init.OverSampling = UART_OVERSAMPLING_16;
  huart2.Init.OneBitSampling = UART_ONE_BIT_SAMPLE_DISABLE;
  huart2.AdvancedInit.AdvFeatureInit = UART_ADVFEATURE_NO_INIT;
  if (HAL_UART_Init(&huart2) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN USART2_Init 2 */

  /* USER CODE END USART2_Init 2 */

}

/**
  * @brief GPIO Initialization Function
  * @param None
  * @retval None
  */
static void MX_GPIO_Init(void)
{
  GPIO_InitTypeDef GPIO_InitStruct = {0};

  /* GPIO Ports Clock Enable */
  __HAL_RCC_GPIOF_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  __HAL_RCC_GPIOB_CLK_ENABLE();

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOF, Direction3_Pin|Step3_Pin, GPIO_PIN_RESET);

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOA, Relay1_Pin|Relay2_Pin|Relay3_Pin|Relay4_Pin, GPIO_PIN_RESET);

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOB, Step1_Pin|Direction1_Pin|Step2_Pin|Direction2_Pin, GPIO_PIN_RESET);

  /*Configure GPIO pins : Direction3_Pin Step3_Pin */
  GPIO_InitStruct.Pin = Direction3_Pin|Step3_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOF, &GPIO_InitStruct);

  /*Configure GPIO pins : Relay1_Pin Relay2_Pin Relay3_Pin Relay4_Pin */
  GPIO_InitStruct.Pin = Relay1_Pin|Relay2_Pin|Relay3_Pin|Relay4_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

  /*Configure GPIO pins : Step1_Pin Direction1_Pin Step2_Pin Direction2_Pin */
  GPIO_InitStruct.Pin = Step1_Pin|Direction1_Pin|Step2_Pin|Direction2_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

}

/* USER CODE BEGIN 4 */

/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @retval None
  */
void Error_Handler(void)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */
  __disable_irq();
  while (1)
  {
  }
  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t *file, uint32_t line)
{
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     ex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */
